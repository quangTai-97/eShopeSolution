using eShopSolution.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class PagingRequestbase : RequestBase 
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}
