using eShopSolution.Application.Catalog.Products.Dtos.Manage;
using eShopSolution.Application.Catalog.Products.Dtos.Public;
using eShopSolution.Application.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IPublicProductService
    {
       Task <PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductpagingRequest request);
    }
}
