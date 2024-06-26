using Microsoft.IdentityModel.Tokens;
using Pizzeria.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pizzeria.Services
{
    public class JWTauthService
    {
        private readonly UserService _userService;

        public JWTauthService (UserService userService)
        {
            _userService = userService;
        }

        public string GenerateJwtToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("qD3zq5CnH9vZfL0ZRQkZkDyxFbLz9WyKFesT9yXpbNs=\r\n");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("qD3zq5CnH9vZfL0ZRQkZkDyxFbLz9WyKFesT9yXpbNs=\r\n");

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero 
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                return userId;
            }
            catch
            {
                return null;
            }
        }

        private string GetTokenFromRequest (HttpContext httpContext)
        {
            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader == null || !authorizationHeader.StartsWith("Bearer "))
            {
                return null;
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            return token;          
        }

        public int GetUserIdFromRequest (HttpContext httpContext)
        {
            string token = GetTokenFromRequest(httpContext);
            var userId = GetUserIdFromToken(token);
            if (userId == null) 
            {
                return 0;
            }
            return Convert.ToInt32(userId);
        }

        public User GetUserFromRequest(HttpContext httpContext)
        {
            int userID = GetUserIdFromRequest(httpContext);
            User user = _userService.GetUserById(userID);
            return user;
        }

        public bool CheckIfLogged(HttpContext httpContext)
        {
            return GetUserFromRequest(httpContext) != null;
        }

        public bool CheckIfAdmin(HttpContext httpContext)
        {
            User user = GetUserFromRequest(httpContext);
            return user != null && user.Role == RoleEnum.ADMIN;
        }

        public bool CheckIfAdmin(User user)
        {
            return user != null && user.Role == RoleEnum.ADMIN;
        }


    }
}
