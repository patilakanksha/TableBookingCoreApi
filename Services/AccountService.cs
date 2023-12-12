using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TableBooking.Interfaces;
using TableBooking.Models;
using TableBooking.ViewModels;

namespace TableBooking.Services
{
    public class AccountService
    {
        private readonly IConfiguration _config;
        private readonly IAccountRepository _accountRepository;

        /// <summary>
        /// Constructor to inject dependencies
        /// </summary>
        /// <param name="config"></param>
        /// <param name="accountRepository"></param>
        public AccountService(
            IConfiguration config, 
            IAccountRepository accountRepository)
        {
            _config = config;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Generate both access and refresh tokens for a user.
        /// </summary>
        public Tokens GenerateToken(User user)
        {
            return GenerateJWTTokens(user);
        }

        /// <summary>
        /// Generate a new refresh token for a user.
        /// </summary>
        public Tokens GenerateRefreshToken(User user)
        {
            return GenerateJWTTokens(user);
        }

        /// <summary>
        /// Generate JWT tokens for a user based on the provided user details.
        /// </summary>
        public Tokens GenerateJWTTokens(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Email),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            var refreshToken = GenerateRefreshToken();
            return new Tokens { AccessToken = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = refreshToken };

        }

        /// <summary>
        /// Generate a random refresh token.
        /// </summary>
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// Retrieve a claims principal from an expired JWT token.
        /// </summary>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }


        /// <summary>
        /// Authenticate a user based on login credentials.
        /// </summary>
        public async Task<User?> Authenticate(LoginViewModel userLogin)
        {
            var currentUser = await _accountRepository.AuthenticateUser(userLogin);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    }
}
