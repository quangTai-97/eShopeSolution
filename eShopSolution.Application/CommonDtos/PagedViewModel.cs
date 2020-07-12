using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.CommonDtos
{
    public class PagedViewModel<T>
    {
        List<T> Items { set; get; }

        public int TotalRecord { set; get; }

    }
}
