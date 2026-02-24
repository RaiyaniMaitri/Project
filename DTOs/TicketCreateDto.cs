using System.ComponentModel.DataAnnotations;
using Backend.Models.Enums;

namespace Backend.DTOs
{
    public class TicketCreateDto
    {
        [Required]
        [MinLength(5)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(10)]
        public string Description { get; set; } = string.Empty;

        public TicketPriority Priority { get; set; } = TicketPriority.MEDIUM;
    }
}