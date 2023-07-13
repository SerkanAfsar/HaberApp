using HaberApp.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace HaberApp.WebService.CustomFilters
{
    public class CustomFilterAttribute<T> : IActionFilter where T : class
    {
        private readonly ResponseResult<T> responseResult;
        public CustomFilterAttribute()
        {
            this.responseResult = new ResponseResult<T>();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                this.responseResult.StatusCode = System.Net.HttpStatusCode.BadRequest;
                this.responseResult.Success = false;
                context.ModelState.Select(a => a.Value).SelectMany(a => a.Errors).Select(a => a.ErrorMessage).ToList().ForEach(a =>
                {
                    this.responseResult.ErrorList.Add(a);
                });
                context.Result = new ObjectResult(this.responseResult)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                return;
            }
        }
    }
}
