using System.ComponentModel.DataAnnotations;
using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Authorize(Roles ="MANAGER")]
    [Route("api/[controller]")]
    [ApiController]
    [Route("users")]
    public class UserControllerAPI : ControllerBase
    {

        private readonly AppDbContext _context;

        public UserControllerAPI (AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async Task<IActionResult> Create(UserCreateDto dto)
        {
            var user = new User()
            {
                Name = dto.Name!,
                Email = dto.Email!,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = dto.RoleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {

            return Ok(await _context.Users.Include(u => u.Role).ToListAsync());
        }



    }
}
