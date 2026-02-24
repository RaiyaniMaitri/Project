using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STM.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = "OPEN";

        public string Priority { get; set; } = "MEDIUM";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Created By
        public int CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public Users? Creator { get; set; }

        // Assigned To
        public int? AssignedTo { get; set; }

        [ForeignKey("AssignedTo")]
        public Users? Assignee { get; set; }

        public ICollection<TicketComment>? Comments { get; set; }
        public ICollection<TicketStatusLog>? StatusLogs { get; set; }
    }
}