using eShopSolution.Application.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Products.Dtos.Manage
{
    public class GetProductPagingRequest : PagingRequestbase
    {
        public string keyword { get; set; }

        public List<int> categoryIds { get; set; }


    }
}
