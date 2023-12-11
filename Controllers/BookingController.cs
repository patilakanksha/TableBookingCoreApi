using TableBooking.Data;
using TableBooking.Interfaces;
using TableBooking.Models;
using TableBooking.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using TableBooking.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreFinalTestBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Guest")]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookingRepository _bookingRepository;
        private readonly IGenericRepository<Booking> _genericRepository;

        public BookingController(ApplicationDbContext context, IBookingRepository bookingRepository, IGenericRepository<Booking> genericRepository)
        {
            _context = context;
            _bookingRepository = bookingRepository;
            _genericRepository = genericRepository;
        }
        // GET: api/<BookingController>
        [HttpGet("List")]
        public async Task<IEnumerable> GetAll()
        {
            var bookingsList = await _bookingRepository.GetAllBookings();
            return bookingsList;
        }

        // GET api/<BookingController>/5
        [HttpPost("BookingGetById")]
        public async Task<ActionResult<Booking>> Details([FromBody] IdViewModel request)
        {
            return await _bookingRepository.GetBookingById(request.Id);
        }

        // POST api/<BookingController>
        [HttpPost]
        public  async Task<IActionResult> Create([FromBody] Booking bookingData)
        {
            bookingData.Id = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                await _genericRepository.Create(bookingData);
                await _genericRepository.SaveChanges();
                return Ok();
            }
            return BadRequest(new
            {
                Status = "Error while updating table",
            });
        }

        // PUT api/<BookingController>/5
        [HttpPost("Update")]
        public IActionResult Put([FromBody] Booking updatedBookingData)
        {
            _genericRepository.Edit(updatedBookingData);
            _genericRepository.SaveChanges();
            return Ok(new { Status = "Booking updated successfully" });
        }

        // DELETE api/<BookingController>/5
        [HttpPost("Delete")]
        public async Task<ActionResult> Delete([FromBody] IdViewModel request)
        {
            Booking bookingForDelete = await _genericRepository.GetByIdAsync(request.Id);
            _genericRepository.Delete(bookingForDelete);
            _genericRepository.SaveChanges();
            return Ok();
        }
    }
}
