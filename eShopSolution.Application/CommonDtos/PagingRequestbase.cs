using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.CommonDtos
{
    public class PagingRequestbase
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}
