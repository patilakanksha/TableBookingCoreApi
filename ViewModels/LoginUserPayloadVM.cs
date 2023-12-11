using System.ComponentModel.DataAnnotations;

namespace TableBooking.ViewModels
{
    public class LoginUserPayloadVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
