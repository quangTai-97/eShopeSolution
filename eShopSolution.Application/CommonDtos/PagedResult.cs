using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.CommonDtos
{
    public class PagedResult<T>
    {
        public List<T> Items { set; get; }

        public int TotalRecord { set; get; }

    }
}
