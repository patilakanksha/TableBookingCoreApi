using TableBooking.Data;
using TableBooking.Interfaces;
using TableBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using TableBooking.ViewModels;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreFinalTestBackend.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TableController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericRepository<TableBooking.Models.Table> _genericRepository;

        public TableController(ApplicationDbContext context, IGenericRepository<TableBooking.Models.Table> genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }


        // GET: api/<TableController>
        [AllowAnonymous]
        [HttpGet("List")]
        public async Task<IEnumerable> GetAll()
        {
            // Retrieve all tables asynchronously
            var tablesList = await _genericRepository.GeAllAsync();
            return tablesList;
        }

        // GET api/<TableController>/5
        [HttpPost("{id}")]
        public async Task<ActionResult<TableBooking.Models.Table>> Get([FromBody] IdViewModel request)
        {
            var tableDetails = await _genericRepository.GetByIdAsync(request.Id);
            return tableDetails;
        }

        // POST api/<TableController>
        [AllowAnonymous]
        [HttpPost("Add")]
        public async Task<ActionResult> Post([FromBody] TableBooking.Models.Table tableData)
        {
            tableData.Id = Guid.NewGuid();
            if (ModelState.IsValid)
            {               
                await _genericRepository.Create(tableData);
                await _genericRepository.SaveChanges();
                return Ok();
            }
            return BadRequest(new
            {
                Status = "Error while updating table",
            });
        }

        // PUT api/<TableController>/5
        [AllowAnonymous]
        [HttpPost("Update")]
        public IActionResult Put([FromBody] TableBooking.Models.Table tableForUpdate)
        {
            _genericRepository.Edit(tableForUpdate);
            _genericRepository.SaveChanges();
            return Ok(new { Status = "Table updated successfully" });
        }

        // DELETE api/<TableController>/5
        [AllowAnonymous]
        [HttpPost("Delete")]
        public async Task<ActionResult> Delete([FromBody] IdViewModel request)
        {
            TableBooking.Models.Table tableForDelete = await _genericRepository.GetByIdAsync(request.Id);
            _genericRepository.Delete(tableForDelete);
            _genericRepository.SaveChanges();
            return Ok();
        }
    }
}
