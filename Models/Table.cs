namespace TableBooking.Models
{
    public class Table
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
