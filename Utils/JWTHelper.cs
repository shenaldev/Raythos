using System.Security.Claims;

namespace Raythos.Utils
{
    public class JWTHelper
    {
        public static string? GetEmailFromJWT(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
