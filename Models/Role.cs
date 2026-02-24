using System.ComponentModel.DataAnnotations;

namespace STM.Models
{
    public class Role
    {
        public int id { get; set; }
        
        [Required]
        public string name { get; set; }// Manager , Support , User
        
        public ICollection<Users>? Users { get; set; }
    }
}