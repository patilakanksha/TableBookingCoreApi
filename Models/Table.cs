
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TableBooking.Models
{
    public class Table
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        [NotMapped]
        public List<Booking>? Bookings { get; set; }
    }
}
