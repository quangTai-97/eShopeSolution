using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.Product
{
    public class GetManageProductpagingRequest : PagingRequestbase
    {
        public string keyword { get; set; }
        public List<int> categoryIds { get; set; }
    }
}
