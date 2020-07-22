using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.System
{
    public class GetUserPagingRequest : PagingRequestbase
    {
        public string Keyword { get; set; }
    }
}
