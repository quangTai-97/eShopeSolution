using eShopSolution.Application.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Products.Dtos.Public
{
    public class GetProductpagingRequest : PagingRequestbase
    {
        public int? categoryId { get; set; }
    }
}
