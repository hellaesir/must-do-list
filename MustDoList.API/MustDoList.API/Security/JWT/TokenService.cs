using Microsoft.IdentityModel.Tokens;
using MustDoList.Config.Configuration;
using MustDoList.Dto.User;
using MustDoList.Service.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MustDoList.API.Security.JWT
{
    public static class TokenService
    {
        private static int HOURS_VALID_TOKEN = 12;
        private static int HOURS_VALID_REFRESHTOKEN = 120;

        public static string GenerateToken(IConfiguration configuration, UserAuthenticationDTO user)
        {
            var expireDate = DateTime.UtcNow.AddHours(HOURS_VALID_TOKEN);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.loadAppSettings(configuration).Authentication.PrivateToken);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("name", user.Name),
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("email", user.Email),
                    new Claim("expires_at", expireDate.Ticks.ToString()),
                }),
                Expires = expireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Método pra gerar token a partir das claims de outro token,´para não ter q ficar acessando o BD!
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static string GenerateToken(IConfiguration configuration, IEnumerable<Claim> claims)
        {
            var expireDate = DateTime.UtcNow.AddHours(HOURS_VALID_TOKEN);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.loadAppSettings(configuration).Authentication.PrivateToken);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// O refresh token nada mais é que uma string unica
        /// </summary>
        /// <returns></returns>
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Método para pegar os claims a partir de um token
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="SecurityTokenException"></exception>
        public static ClaimsPrincipal GetPrincipalFromExpiredToken(IConfiguration configuration, string token)
        {
            var key = Encoding.ASCII.GetBytes(AppSettings.loadAppSettings(configuration).Authentication.PrivateToken);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public static async Task SaveRefreshToken(IUserService userService, string email, string refreshToken)
        {
            await userService.SaveRefreshToken(email, refreshToken);
        }

        public static async Task RevokeRefreshToken(IUserService userService, string email)
        {
            await userService.RevokeRefreshToken(email);
        }

        public static async Task<string> RetrieveRefreshToken(IUserService userService, string email)
        {
            var refreshToken = await userService.RetrieveRefreshToken(email);

            return refreshToken;
        }
    }
}