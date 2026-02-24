using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class TicketStatusLog
    {
        public int Id { get; set; }

        public int TicketId { get; set; }
        public Ticket? Ticket { get; set; }

        public string OldStatus { get; set; } = string.Empty;

        public string NewStatus { get; set; } = string.Empty;

        public int ChangedBy { get; set; }
        public User? ChangedByUser { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.Now;
    }
}