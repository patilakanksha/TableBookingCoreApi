using TableBooking.Data;
using TableBooking.Interfaces;
using TableBooking.Models;
using TableBooking.Services;
using TableBooking.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TableBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ApplicationDbContext _context;
        private readonly AccountService _accountService;

        // Constructor to inject dependencies
        public AccountController(
            IAccountRepository accountRepository, 
            IGenericRepository<User> userRepository, 
            ApplicationDbContext context, 
            AccountService accountService)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _context = context;
            _accountService = accountService;
        }

        /// <summary>
        /// Action method to handle user login
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>Return the generated token</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel userLogin)
        {
            var user = await _accountService.Authenticate(userLogin);
            if (user != null)
            {
                var response = new TokenPayloadVM();
                // Generate token and update refresh token information in the database
                var token = _accountService.GenerateToken(user);
                user.RefreshAccessToken = token.RefreshToken;
                user.RefreshTokenExpiryDate = DateTime.Now.AddDays(1);

                _context.SaveChanges();
                
                var loginUser = new LoginUserPayloadVM {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role,
                };
                response.Token = token;
                response.User = loginUser;
                return Ok(response);
            }
            return NotFound("user not found");
        }

        /// <summary>
        /// Action method to refresh the access token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Return the new JWT token</returns>
        [AllowAnonymous]
        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] Tokens token)
        {
            // Get user details based on the expired access token
            var principal = _accountService.GetPrincipalFromExpiredToken(token.AccessToken);
            var userName = principal.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value;
            var userDetails = await _accountRepository.GetUserByUserName(userName);

            //Check if user details are valid and refresh token is not expired
            if (userDetails == null ||
                (userDetails != null && ((userDetails.RefreshAccessToken != token.RefreshToken) ||
                                        (userDetails.RefreshAccessToken == token.RefreshToken && userDetails.RefreshTokenExpiryDate < DateTime.Now))))
            {
                return Unauthorized("Invalid attempl!");
            }

            // Generate a new refresh token and update the user's refresh token information in the database
            var newJwtToken = _accountService.GenerateRefreshToken(userDetails);
            if (newJwtToken == null || (newJwtToken != null && (newJwtToken.AccessToken == null || newJwtToken.RefreshToken == null)))
            {
                return Unauthorized("Invalid attempl!");
            }

            // Update refresh token information in the database
            userDetails.RefreshAccessToken = newJwtToken.RefreshToken;
            userDetails.RefreshTokenExpiryDate = DateTime.Now.AddDays(1);
            _context.SaveChanges();
            return Ok(newJwtToken);
        }
    }
}
