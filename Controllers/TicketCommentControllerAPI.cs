using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using System.Security.Claims;
using Backend.DTOs;
using Backend.Models;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("comments")]
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CommentsController(AppDbContext context)
        {
            _context = context;
        }

        private int CurrentUserId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        private string CurrentRole =>
            User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;


        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, CommentDto dto)
        {
            var comment = await _context.TicketComments.FindAsync(id);
            if (comment == null) return NotFound();

            if (CurrentRole != "MANAGER" && comment.UserId != CurrentUserId)
                return Forbid();

            comment.Comment = dto.Comment!;
            await _context.SaveChangesAsync();

            return Ok(comment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _context.TicketComments.FindAsync(id);
            if (comment == null) return NotFound();

            if (CurrentRole != "MANAGER" && comment.UserId != CurrentUserId)
                return Forbid();

            _context.TicketComments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}