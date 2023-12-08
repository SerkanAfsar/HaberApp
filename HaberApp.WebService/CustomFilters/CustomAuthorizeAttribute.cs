using HaberApp.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace HaberApp.WebService.CustomFilters
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(params string[] claim) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { claim };
        }
    }
    public class AuthorizeFilter : IAuthorizationFilter
    {
        readonly string[] _claim;
        private readonly ResponseResult<string> responseResult;
        public AuthorizeFilter(params string[] claim)
        {
            _claim = claim;
            this.responseResult = new ResponseResult<string>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var endpoint = context.HttpContext.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return;
            }



            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            if (!IsAuthenticated)
            {
                this.responseResult.Success = false;
                this.responseResult.ErrorList.Add("Unauthorized.. Invalid EMail Or Password");
                this.responseResult.StatusCode = HttpStatusCode.Unauthorized;
                context.Result = new UnauthorizedObjectResult(this.responseResult);
            }

            if (IsAuthenticated && _claim != null)
            {
                var claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;
                bool flagClaim = false;
                foreach (var item in _claim)
                {
                    if (claimsIndentity.HasClaim(ClaimTypes.Role, item) || claimsIndentity.HasClaim("Permission", item))
                    {
                        flagClaim = true;
                        break;

                    }

                }
                if (!flagClaim)
                {
                    this.responseResult.Success = false;
                    this.responseResult.ErrorList.Add("Forbidden For This");
                    this.responseResult.StatusCode = HttpStatusCode.Forbidden;
                    context.Result = new ObjectResult(this.responseResult)
                    {
                        StatusCode = (int)HttpStatusCode.Forbidden
                    };
                }
            }
        }
    }
}
