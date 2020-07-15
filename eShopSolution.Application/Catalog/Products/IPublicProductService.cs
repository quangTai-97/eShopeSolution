﻿using eShopSolution.ViewModels.Catalog.Product;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IPublicProductService
    {
       Task <PagedResult<ProductViewModel>> GetAllByCategoryId(string languaegId,GetPublicProductpagingRequest request);

 
    }
}
