using TableBooking.Models;

namespace TableBooking.Interfaces
{
    public interface IBookingRepository
    {
        public Task<IEnumerable<Booking>> GetAllBookings();
        public Task<Booking> GetBookingById(Guid id);
        //public Task CreateBookingAsync(Booking booking);
    }
}
