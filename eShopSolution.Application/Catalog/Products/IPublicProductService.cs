using eShopSolution.Application.Catalog.Products.Dtos.Manage;
using eShopSolution.Application.Catalog.Products.Dtos.Public;
using eShopSolution.Application.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        PagedResult<ProductViewModel> GetAllByCategoryId(GetProductpagingRequest request);
    }
}
