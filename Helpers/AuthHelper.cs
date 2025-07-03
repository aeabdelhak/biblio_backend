using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BiblioPfe.helpers
{
    public class AuthHelper(
        JWTParams _jWTParams,
        IUserDa userDA,
        IHttpContextAccessor _contextAccessor
    )
    {
        private int keySize = 64;
        private int iterations = 350000;
        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;

        public string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize
            );
            return Convert.ToHexString(hash);
        }

        public bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                hashAlgorithm,
                keySize
            );
            return CryptographicOperations.FixedTimeEquals(
                hashToCompare,
                Convert.FromHexString(hash)
            );
        }

        public JwtClaims GetAuthClaims()
        {
            var userId = Guid.Parse(
                _contextAccessor.HttpContext!.User.FindFirstValue("USER_ID")!.ToString()
            );
            var role = Enum.Parse<UserRole>(
                _contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Role)!.ToString()
            );

            var DisplayName = _contextAccessor
                .HttpContext!.User.FindFirstValue(JwtRegisteredClaimNames.Name)!
                .ToString();
            var reftokenId = Guid.Parse(
                _contextAccessor
                    .HttpContext!.User.FindFirstValue(JwtRegisteredClaimNames.Jti)!
                    .ToString()
            );

            return new JwtClaims()
            {
                UserId = userId,
                DisplayName = DisplayName,
                RefTokenId = reftokenId,
                Role = role
            };
        }

        public string GenerateAccessToken(User user, Guid refreshTokenId)
        {
            var authClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Name, user.DisplayName ?? ""),
                new("USER_ID", user.Id.ToString()),
                new(ClaimTypes.Role, user.Role.ToString()),
                new(JwtRegisteredClaimNames.Typ, "ACCESS"),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, refreshTokenId.ToString()),
            };

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jWTParams.Secret)
            );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jWTParams.ValidIssuer,
                Audience = _jWTParams.ValidAudience,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                    authSigningKey,
                    SecurityAlgorithms.HmacSha256
                ),
                Subject = new ClaimsIdentity(authClaims)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(User user, Guid refTokenId)
        {
            var authClaims = new List<Claim>
            {
                new("USER_ID", user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, refTokenId.ToString()),
                new(ClaimTypes.Role, user.Role.ToString()),
                new(JwtRegisteredClaimNames.Typ, "REFRESH"),
            };
            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jWTParams.Secret)
            );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jWTParams.ValidIssuer,
                Audience = _jWTParams.ValidAudience,
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(
                    authSigningKey,
                    SecurityAlgorithms.HmacSha256
                ),
                Subject = new ClaimsIdentity(authClaims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = _jWTParams.ValidAudience,
                ValidIssuer = _jWTParams.ValidIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(_jWTParams.Secret)
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                if (token.IsNullOrEmpty())
                    return null;
                var isValid = tokenHandler.ValidateToken(
                    token,
                    validationParameters,
                    out var securityToken
                );

                if (securityToken is JwtSecurityToken jwtSecurityToken)
                {
                    var isAlgorithmValid = jwtSecurityToken.Header.Alg.Equals(
                        SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase
                    );

                    if (!isAlgorithmValid)
                    {
                        return null;
                    }

                    return isValid;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
