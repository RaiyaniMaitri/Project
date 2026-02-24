using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using STM.Models;

namespace STM.Models
{
    public class Users
    {
        public int Id { get; set; }
        
        [Required , MaxLength(50)]
        public string Name { get; set; }
        
        [Required , EmailAddress]
        public string Email { get; set; }
        
        [Required , MinLength(6)]
        public string Password { get; set; }
        
        public DateTime Created { get; set; } = DateTime.Now;
        
        //ForfinKry
        public int RoleId { get; set; }
        
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }
        
        public ICollection<Ticket>? CreatedTickets { get; set; }   
        public ICollection<Ticket>? AssignedTickets { get; set; }
        public ICollection<TicketComment>? Comments { get; set; }
        public ICollection<TicketStatusLog>?  StatusLogs { get; set; }
        
        
        
    }
}

