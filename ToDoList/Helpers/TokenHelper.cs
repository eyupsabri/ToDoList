using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ToDoList.Helpers
{
    public class TokenHelper
    {

        private bool _isExpired;
        private bool _isValidToken;
        private string _tokenType;
        private string _email;

        public bool IsExpired => _isExpired;
        public bool IsValidToken => _isValidToken;
        public string TokenType => _tokenType;
        public string Email => _email;

        public TokenHelper(bool isExpired, bool isValidToken, string tokenType, string email)
        {
            _isExpired = isExpired;
            _isValidToken = isValidToken;
            _tokenType = tokenType;
            _email = email;
        }

        public static TokenHelper ValidateToken(string token, string _secretKey, string _jwtIssuer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtIssuer,
                ValidAudience = _jwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                var emailClaim = principal.FindFirst(ClaimTypes.Email);
                var tokenTypeClaim = principal.FindFirst("token_type");
                var result = validatedToken is JwtSecurityToken jwtToken && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                if (result)
                {
                    return new TokenHelper(false, true, tokenTypeClaim.Value, emailClaim.Value);

                }

            }
            catch (SecurityTokenException ex)
            {

                if (ex.Message.Contains("Lifetime validation failed."))
                {
                    var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                    var emailClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                    var tokenTypeClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == "token_type");
                    return new TokenHelper(true, true, tokenTypeClaim.Value, "");

                }
                else
                {
                    return new TokenHelper(false, false, "", "");
                }
            }
            catch (SecurityTokenMalformedException e)
            {
                return new TokenHelper(false, false, "", "");
            }
            return new TokenHelper(false, false, "", "");
        }

        public static string GenerateToken(string email, bool isRefreshToken, string _secretKey, string _jwtIssuer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenType = isRefreshToken ? "refresh" : "access";

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("token_type", tokenType)
            };



            var token = new JwtSecurityToken(_jwtIssuer,
              _jwtIssuer,
              claims,
              expires: isRefreshToken ? DateTime.Now.AddDays(30) : DateTime.Now.AddMinutes(10),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
