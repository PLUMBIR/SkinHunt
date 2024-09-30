using System.IdentityModel.Tokens.Jwt;

namespace SkinHunt.Application.Extensions
{
    public class JwtTokenHandler
    {
        public static async Task<string> GetIdFromTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtSecurityToken != null)
            {
                return jwtSecurityToken.Claims.ToList()[2].Value;
            }

            return null;
        }
    }
}
