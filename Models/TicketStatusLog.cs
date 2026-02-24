using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using STM.Models;

namespace STM.Models
{
    public class TicketStatusLog
    {
        public int Id { get; set; }
        
        [Required]
        public int OldStatus { get; set; }
        
        [Required]
        public int NewStatus { get; set; }
        
       public DateTime ChangedAt { get; set; } = DateTime.Now;
       
       //Ticket FK
       public int TicketId { get; set; }
       
       [ForeignKey("TicketId")]
       public Ticket? Ticket { get; set; }
       
       //Changes By
       public int ChangedBy { get; set; }
       
       [ForeignKey("ChangedBy")]
       public Users? Users { get; set; }
       
        
    }
}

