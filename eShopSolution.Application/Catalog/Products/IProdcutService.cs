using eShopSolution.ViewModels.Catalog.Product;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IProdcutService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);

        Task<ProductViewModel> GetById(int productId,string languageId);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> UpdateStock(int productId, int addedQuantity);

        Task AddViewCount(int productId);

        Task<int> AddImage(int productId, ProductImageCreateRequest request);

        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

        Task<int> DeleteImage(int imageId);
        Task<ProductImageViewModel> GetByImageId(int imageId);

        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductpagingRequest request);

        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string languaegId, GetPublicProductpagingRequest request);
    }
}
