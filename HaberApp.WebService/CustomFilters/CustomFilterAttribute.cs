﻿using HaberApp.Core.DTOs;
using HaberApp.Core.Models.Abstract;
using HaberApp.Core.Services;
using HaberApp.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace HaberApp.WebService.CustomFilters
{
    public class CustomFilterAttribute<Domain, RequestDto, ResponseDto> : IAsyncActionFilter where Domain : BaseEntity where RequestDto : BaseDto where ResponseDto : BaseDto
    {
        private readonly ResponseResult<ResponseDto> responseResult;
        private readonly IServiceBase<Domain, RequestDto, ResponseDto> serviceBase;

        public CustomFilterAttribute(IServiceBase<Domain, RequestDto, ResponseDto> serviceBase)
        {
            this.responseResult = new ResponseResult<ResponseDto>();
            this.serviceBase = serviceBase;

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.ContainsKey("id"))
            {
                var id = context.ActionArguments["id"];
                var result = await serviceBase.GetByIdAsync(Convert.ToInt32(id));
                if (result.Success)
                {
                    context.HttpContext.Items["result"] = result;
                }
            }

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

            }

            await next();
        }
    }
}
