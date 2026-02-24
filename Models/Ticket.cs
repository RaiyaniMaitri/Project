using System.ComponentModel.DataAnnotations;
using Backend.Models.Enums;

namespace Backend.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(10)]
        public string Description { get; set; } = string.Empty;

        public TicketStatus Status { get; set; } = TicketStatus.OPEN;

        public TicketPriority Priority { get; set; } = TicketPriority.MEDIUM;

        public int CreatedBy { get; set; }
        public User? Creator { get; set; }

        public int? AssignedTo { get; set; }
        public User? AssignedUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<TicketComment>? Comments { get; set; }
        public ICollection<TicketStatusLog>? StatusLogs { get; set; }
    }
}