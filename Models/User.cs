using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; }
        public Role? Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}