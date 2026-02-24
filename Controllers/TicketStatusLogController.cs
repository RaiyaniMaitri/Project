using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STM.Models;
using STM.Data;

namespace STM.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class TicketStatusLogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TicketStatusLogController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketStatusLog ticketStatusLog)
        {
            _context.TicketStatusLogs.Add(ticketStatusLog);
            await _context.SaveChangesAsync();
            return Ok(ticketStatusLog);
        }

        [HttpGet("ticket/{ticketid}")]
        public async Task<IActionResult> Get(int ticketid)
        {
            return Ok(await _context.TicketStatusLogs
                .Where(x => x.TicketId == ticketid)
                .Include(x => x.Users)
                .ToListAsync());

        }
        
    }
    
}
