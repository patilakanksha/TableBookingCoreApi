using TableBooking.Data;
using TableBooking.Interfaces;
using TableBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TableBooking.Repositories
{
    public class BookingRepository:GenericRepository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
             return await _context.Bookings
                            .Include(x => x.User)
                            .Include(x => x.Table)
                            .ToListAsync();
        }

        public async Task<Booking> GetBookingById(Guid id)
        {
            return await _context.Bookings.Include(x => x.User)
                            .Include(x => x.Table).FirstAsync(x => x.Id == id);
        }

        //public async Task CreateBookingAsync(Booking booking)
        //{
        //    await _context.Bookings.AddAsync(booking);
        //}
    }
}
