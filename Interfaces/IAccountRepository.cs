using TableBooking.Models;
using TableBooking.ViewModels;

namespace TableBooking.Interfaces
{
    /// <summary>
    /// Interface defining operations related to user accounts.
    /// </summary>
    public interface IAccountRepository
    {
        public Task<User?> AuthenticateUser(LoginViewModel loginCredentials);
        public Task<User?> GetUserByUserName(string userName);
    }
}
