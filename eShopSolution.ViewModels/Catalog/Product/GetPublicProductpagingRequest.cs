using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.Product
{
    public class GetPublicProductpagingRequest : PagingRequestbase
    {
        public string LanguaegId { get; set; }
        public int? categoryId { get; set; }
    }
}
