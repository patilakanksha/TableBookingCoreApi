using TableBooking.Models;

namespace TableBooking.ViewModels
{
    public class TokenPayloadVM
    {
        public Tokens Token { get; set; }
        public LoginUserPayloadVM User{ get; set; }
    }
}
