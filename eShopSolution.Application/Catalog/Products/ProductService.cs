using eShopSolution.ViewModels.Catalog.Product;
using eShopSolution.ViewModels.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.IO;
using eShopSolution.Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace eShopSolution.Application.Catalog.Products
{
    public class ProductService : IProdcutService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;
        public ProductService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImage(int productId,ProductImageCreateRequest request)
        {
           
            if (productId == 0) throw new eShopException($"Cannot find a product:{productId}");
            var productImg = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                ProductId = productId,
                IsDeafault = request.IsDefault,
                SortOrder = request.SortOrder

            };
            if(request.ImageFile != null)
            {
                productImg.ImagePath = await this.SaveFile(request.ImageFile);
                productImg.FileSize = request.ImageFile.Length;
            }    
          
            _context.ProductImages.Add(productImg);
             await _context.SaveChangesAsync();
            return productImg.Id;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
             await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }
            };
            //Save image
            if(request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDeafault = true,
                        SortOrder = 1

                    }
                };
            }
            _context.Products.Add(product);
             await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if(product == null) throw new eShopException($"Cannot find a product:{productId}");
            _context.Products.Remove(product);
           return await _context.SaveChangesAsync();
        }



        public async Task<int> DeleteImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null) throw new eShopException($"Cannot find a product:{imageId}");
            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string LanguaegId, GetPublicProductpagingRequest request)
        {
            // 1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == LanguaegId
                        select new { p, pt, pic };
            // 2. filter
            if (request.categoryId.HasValue && request.categoryId.Value > 0)
                query = query.Where(p => p.pic.CategoryId == request.categoryId);

            // 3.Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.pageIndex - 1) * request.pageSize)
                .Take(request.pageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            // 4.Select  and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductpagingRequest request)
        {

            // 1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt,pic };
            // 2. filter
            if(!string.IsNullOrEmpty(request.keyword))
                query = query.Where(x => x.pt.Name.Contains(request.keyword));
            if (request.categoryIds.Count > 0)
                query = query.Where(p => request.categoryIds.Contains(p.pic.CategoryId));

            // 3.Paging
            int totalRow =await query.CountAsync();
            var data = await query.Skip((request.pageIndex - 1) * request.pageSize)
                .Take(request.pageSize)
                .Select(x=>new ProductViewModel() { 
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            // 4.Select  and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<ProductViewModel> GetById(int productId, string languageId)
        {
            var product = await _context.Products.FindAsync(productId);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == languageId);
           
            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                DateCreated = product.DateCreated,
                LanguageId = productTranslation.LanguageId,
                Details = productTranslation != null ? productTranslation.Details : null,
                Name = productTranslation != null ? productTranslation.Name : null,
                Description = productTranslation != null ? productTranslation.Description : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
                SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
                SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null,
                Stock = product.Stock,
                ViewCount = product.ViewCount
            };
            return productViewModel;

            
        }

        public async Task<ProductImageViewModel> GetByImageId(int imageId)
        {
            if (imageId == 0) throw new eShopException($"Cannot find a product:{imageId}");
            var prodcutImage = await _context.ProductImages.FindAsync(imageId);
            var Image = new ProductImageViewModel()
            {
                Caption = prodcutImage.Caption,
                DateCreated = prodcutImage.DateCreated,
                FileSize = prodcutImage.FileSize,
                ImagePath = prodcutImage.ImagePath,
                IsDefault = prodcutImage.IsDeafault,
                ProductId = prodcutImage.ProductId,
                SortOrder = prodcutImage.SortOrder
            };
            return Image;
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product =await _context.Products.FindAsync(request.id);
            var productTransaction = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.id && x.LanguageId == request.LanguageId);
            if (product == null) throw new eShopException($"Cannot find a product:{request.id}");

            productTransaction.Name = request.Name;
            productTransaction.SeoAlias = request.SeoAlias;
            productTransaction.SeoDescription = request.SeoDescription;
            productTransaction.SeoTitle = request.SeoTitle;
            productTransaction.Details = request.Details;
            productTransaction.Description = request.Description;

            return await _context.SaveChangesAsync();
        }



  

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (imageId == 0) throw new eShopException($"Cannot find a product:{imageId}");

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }

            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product =await _context.Products.FindAsync(productId);
            if (product == null) throw new eShopException($"Cannot find a product:{productId}");

            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new eShopException($"Cannot find a product:{productId}");

            product.Stock = addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;

        }
    }
}
