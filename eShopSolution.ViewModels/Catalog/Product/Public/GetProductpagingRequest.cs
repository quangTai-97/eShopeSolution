using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.Product.Public
{
    public class GetProductpagingRequest : PagingRequestbase
    {
        public int? categoryId { get; set; }
    }
}
