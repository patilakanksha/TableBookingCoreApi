
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TableBooking.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }  
        public string Password { get; set; }
        public string Role { get; set; }

        [JsonIgnore]
        [NotMapped]
        public List<Booking>? Bookings { get; set; }
    }
}
