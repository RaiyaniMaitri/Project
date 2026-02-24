using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using STM.Models;

namespace STM.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        
        [Required]
        public string Comment { get; set; }
        
        public DateTime Created { get; set; } = DateTime.Now;
        
        //Ticket ForginKry
        public int TicketId { get; set; }
        
        [ForeignKey("TicketId")]
        public Ticket? Ticket { get; set; }
        
        //USer Forgin Key
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public Users? User { get; set; }
        
    }
    
}

