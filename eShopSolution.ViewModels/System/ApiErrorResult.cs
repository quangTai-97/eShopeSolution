using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.System
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public string[] ValidationError { get; set; }
        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }
        //public ApiErrorResult(ModelState validationError)
        //{
        //    IsSuccessed = false;
        //    ValidationError = validationError;
        //}
    }
}
