using Backend.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public class StatusDto
    {
        [Required]
        public TicketStatus Status { get; set; }
    }
}