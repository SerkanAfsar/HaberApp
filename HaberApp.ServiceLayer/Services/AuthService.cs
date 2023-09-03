using HaberApp.Core.Services;
using System.IdentityModel.Tokens.Jwt;

namespace HaberApp.ServiceLayer.Services
{
    public class AuthService : IAuthService
    {
        public string? GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            return jwtToken?.Claims.FirstOrDefault(a => a.Type == claimType)?.Value ?? null;
        }
    }
}
