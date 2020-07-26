using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class ApiResult<T>
    {
        //public ApiResult(bool isSuccessed, string message)
        //{
        //    IsSuccessed = isSuccessed;
        //    Message = message;
        //}
        //public ApiResult(bool isSuccessed, string message, T resultObject)
        //{
        //    IsSuccessed = isSuccessed;
        //    Message = message;
        //    ResultObject = resultObject;
        //}
        public bool IsSuccessed { get; set; }
        public string Message { get; set; }
        public T ResultObject { get; set; }

    }
}
