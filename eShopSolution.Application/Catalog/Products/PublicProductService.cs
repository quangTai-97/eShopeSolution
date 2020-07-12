using eShopSolution.Application.Catalog.Products.Dtos.Manage;
using eShopSolution.Application.Catalog.Products.Dtos.Public;
using eShopSolution.Application.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    { 
        public PagedResult<ProductViewModel> GetAllByCategoryId(GetProductpagingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
