using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using System.Security.Claims;
using Backend.DTOs;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("tickets")]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context = context;
        }

        private int CurrentUserId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        private string CurrentRole =>
            User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

        // POST /tickets
        [HttpPost]
        [Authorize(Roles = "USER,MANAGER")]
        public async Task<IActionResult> Create(TicketCreateDto dto)
        {
            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                CreatedBy = CurrentUserId
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }

        // GET /tickets
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = _context.Tickets.AsQueryable();

            if (CurrentRole == "USER")
                query = query.Where(t => t.CreatedBy == CurrentUserId);

            if (CurrentRole == "SUPPORT")
                query = query.Where(t => t.AssignedTo == CurrentUserId);

            return Ok(await query.ToListAsync());
        }

        // PATCH /tickets/{id}/assign
        [HttpPatch("{id}/assign")]
        [Authorize(Roles = "MANAGER,SUPPORT")]
        public async Task<IActionResult> Assign(int id, AssignDto dto)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            ticket.AssignedTo = dto.UserId;
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }

        // PATCH /tickets/{id}/status
        [HttpPatch("{id}/status")]
        [Authorize(Roles = "MANAGER,SUPPORT")]
        public async Task<IActionResult> ChangeStatus(int id, StatusDto dto)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            var log = new TicketStatusLog
            {
                TicketId = id,
                OldStatus = ticket.Status.ToString(),
                NewStatus = dto.Status.ToString(),
                ChangedBy = CurrentUserId
            };

            ticket.Status = dto.Status;

            _context.TicketStatusLogs.Add(log);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }

        // DELETE /tickets/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST /tickets/{id}/comments
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddComment(int id, CommentDto dto)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            if (!IsAllowed(ticket)) return Forbid();

            var comment = new TicketComment
            {
                TicketId = id,
                UserId = CurrentUserId,
                Comment = dto.Comment!
            };

            _context.TicketComments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }

        // GET /tickets/{id}/comments
        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetComments(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            if (!IsAllowed(ticket)) return Forbid();

            return Ok(await _context.TicketComments
                .Where(c => c.TicketId == id)
                .ToListAsync());
        }

        private bool IsAllowed(Ticket ticket)
        {
            if (CurrentRole == "MANAGER") return true;
            if (CurrentRole == "USER" && ticket.CreatedBy == CurrentUserId) return true;
            if (CurrentRole == "SUPPORT" && ticket.AssignedTo == CurrentUserId) return true;
            return false;
        }
    }
}