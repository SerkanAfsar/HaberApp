using HaberApp.Core.Utils;
using HaberApp.ServiceLayer.Exceptions;
using System.Net;

namespace HaberApp.WebService.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate _next)
        {
            this._next = _next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception error)
            {
                var responseResult = new ResponseResult<string>();
                var response = context.Response;
                response.ContentType = "application/json; charset=utf-8";
                if (error is not null)
                {
                    switch (error)
                    {
                        case NotFoundException:
                            {
                                response.StatusCode = (int)HttpStatusCode.NotFound;
                                responseResult.ErrorList.Add(error.Message);
                                break;
                            }
                        case CustomAppException:
                            {
                                var err = (CustomAppException)error;
                                response.StatusCode = (int)HttpStatusCode.BadRequest;
                                responseResult.ErrorList = err.errorList;
                                break;
                            }
                        default:
                            {
                                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                responseResult.ErrorList.Add(error.Message);
                                break;
                            }
                    }
                    responseResult.StatusCode = (HttpStatusCode)response.StatusCode;
                    responseResult.Success = false;

                    var innerEx = error.InnerException;
                    while (innerEx != null)
                    {
                        responseResult.ErrorList.Add(innerEx.Message);
                        innerEx = innerEx.InnerException;
                    }

                    await response.WriteAsJsonAsync(responseResult);
                }

            }
        }
    }
}
