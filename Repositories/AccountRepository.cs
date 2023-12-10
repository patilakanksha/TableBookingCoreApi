using Microsoft.EntityFrameworkCore;
using TableBooking.Data;
using TableBooking.Interfaces;
using TableBooking.Models;
using TableBooking.ViewModels;

namespace TableBooking.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method to authenticate a user based on login credentials
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns></returns>
        public async Task<User> AuthenticateUser(LoginViewModel loginCredentials)
        {
            var loggedInData = await _dbContext.Users.Where(user => user.Email.ToLower() == loginCredentials.Email.ToLower() &&
                                                                  user.Password == loginCredentials.Password).SingleOrDefaultAsync();
            return loggedInData;
        }

        /// <summary>
        /// Get username from table
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User?> GetUserByUserName(string userName)
        {
            var userData = await _dbContext.Users.Where(user => user.Email.ToLower() == userName.ToLower()).FirstOrDefaultAsync();
            return userData;
        }
    }
}
