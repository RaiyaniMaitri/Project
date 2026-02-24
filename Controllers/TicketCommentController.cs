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

    public class TicketCommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TicketCommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketComment ticketComment)
        {
            _context.TicketComments.Add(ticketComment);
            await _context.SaveChangesAsync();
            return Ok(ticketComment);
        }

        [HttpGet("ticket/{ticketId}")]
        public async Task<IActionResult> GetByTicket(int ticketId)
        {
            return Ok(await _context.TicketComments
                .Where(x => x.TicketId == ticketId)
                .Include(x => x.User)
                .ToListAsync());
        }
    }
}

