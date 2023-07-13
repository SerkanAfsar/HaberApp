using HaberApp.Core.Utils;
using HaberApp.ServiceLayer.Exceptions;
using System.Net;

namespace HaberApp.WebService.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ResponseResult<string> responseResult;

        public ExceptionHandlerMiddleware(RequestDelegate _next)
        {
            this._next = _next;
            this.responseResult = new ResponseResult<string>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json; charset=utf-8";
                if (error is not null)
                {
                    switch (error)
                    {
                        case NotFoundException:
                            {
                                response.StatusCode = (int)HttpStatusCode.NotFound;
                                break;
                            }
                        default:
                            {
                                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                break;
                            }
                    }
                    this.responseResult.StatusCode = (HttpStatusCode)response.StatusCode;
                    this.responseResult.Success = false;
                    this.responseResult.ErrorList.Add(error.Message);
                    var innerEx = error.InnerException;
                    while (innerEx != null)
                    {
                        var err = error.InnerException;
                        this.responseResult.ErrorList.Add(innerEx.Message);
                        innerEx = innerEx.InnerException;
                    }

                    await response.WriteAsJsonAsync(this.responseResult);
                }


            }
        }
    }
}
