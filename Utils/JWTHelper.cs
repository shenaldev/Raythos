using Raythos.Interfaces;
using System.Security.Claims;

namespace Raythos.Utils
{
    public class JWTHelper
    {
        private readonly IUserRepository _userInterface;

        public JWTHelper(IUserRepository userInterface)
        {
            _userInterface = userInterface;
        }

        public static string? GetEmailFromJWT(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<long> GetUserID(ClaimsPrincipal user)
        {
            string? email = GetEmailFromJWT(user);

            if (email == null)
            {
                return -1;
            }

            long userID = await _userInterface.GetUserID(email);
            return userID;
        }
    }
}
