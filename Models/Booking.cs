using System.ComponentModel.DataAnnotations;

namespace TableBooking.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime BookingTime { get; set; }
        public int DurationInMinutes { get; set; }
        public string status { get; set;}        
        public Guid UserId { get; set; }
        public Guid TableId { get; set; }
        public User User { get; set; }
        public Table Table { get; set; }
    }
}
