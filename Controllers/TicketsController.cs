using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STM.Models;
using STM.Data;

namespace STM.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            return (ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return StatusCode(201 , ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ticketFromDb = await _context.Tickets.FindAsync(id);
            if(ticketFromDb == null)  return NotFound();

            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticketFromDb = await _context.Tickets.FindAsync(id);
            if (ticketFromDb == null) return NotFound();
            _context.Tickets.Remove(ticketFromDb);
            await _context.SaveChangesAsync();
            return Ok(ticketFromDb);
            
        }
    }
}