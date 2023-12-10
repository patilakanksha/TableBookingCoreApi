using TableBooking.Data;
using TableBooking.Interfaces;
using TableBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreFinalTestBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericRepository<User> _genericRepository;

        public UserController(ApplicationDbContext context, IGenericRepository<User> genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }
        // GET: api/<UserController>
        [HttpGet("List")]
        public async Task<IEnumerable> GetAll()
        {
            // Retrieve all tables asynchronously
            var tablesList = await _genericRepository.GeAllAsync();
            return tablesList;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            var userDetails = await _genericRepository.GetByIdAsync(id);
            return userDetails;
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User userData)
        {
            userData.Id = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                await _genericRepository.Create(userData);
                await _genericRepository.SaveChanges();
                return Ok();
            }
            return BadRequest(new
            {
                Status = "Error while Adding user",
            });
        }

        // PUT api/<TableController>/5
        [HttpPut("Edit/{id}")]
        public IActionResult Put(Guid id, [FromBody] User userForUpdate)
        {
            _genericRepository.Edit(userForUpdate);
            _genericRepository.SaveChanges();
            return Ok(new { Status = "User updated successfully" });
        }

        // DELETE api/<TableController>/5
        [HttpDelete("Delete{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            User userForDelete = await _genericRepository.GetByIdAsync(id);
            _genericRepository.Delete(userForDelete);
            _genericRepository.SaveChanges();
            return Ok();
        }
    }
}
