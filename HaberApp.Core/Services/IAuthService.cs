namespace HaberApp.Core.Services
{
    public interface IAuthService
    {
        string GetClaim(string token, string claimType);
    }
}
