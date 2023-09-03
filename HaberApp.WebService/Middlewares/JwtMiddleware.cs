using HaberApp.Core.Services;

namespace HaberApp.WebService.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthService authService;
        public JwtMiddleware(RequestDelegate _next, IAuthService authService)
        {
            this._next = _next;
            this.authService = authService;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                string ClientId = this.authService.GetClaim(token, "UserId");
                if (!string.IsNullOrEmpty(ClientId))
                {
                    context.Items["ClientId"] = ClientId;
                }

            }
            await _next(context);
        }
    }
}
