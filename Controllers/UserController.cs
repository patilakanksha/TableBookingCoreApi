using TableBooking.Data;
using TableBooking.Interfaces;
using TableBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using TableBooking.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreFinalTestBackend.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
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
        [HttpPost("GetById")]
        public async Task<ActionResult<User>> Get([FromBody] IdViewModel request)
        {
            var userDetails = await _genericRepository.GetByIdAsync(request.Id);
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
                return Ok(new
                {
                    Status = "User registered successfully",
                });
            }
            return BadRequest(new
            {
                Status = "Error while Adding user",
            });
        }

        // PUT api/<TableController>/5
        [HttpPost("Update")]
        public IActionResult Put([FromBody] User userForUpdate)
        {
            _genericRepository.Edit(userForUpdate);
            _genericRepository.SaveChanges();
            return Ok(new { Status = "User updated successfully" });
        }

        // DELETE api/<TableController>/5
        [HttpPost("Delete")]
        public async Task<ActionResult> Delete(IdViewModel request)
        {
            User userForDelete = await _genericRepository.GetByIdAsync(request.Id);
            _genericRepository.Delete(userForDelete);
            _genericRepository.SaveChanges();
            return Ok();
        }
    }
}
