using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class User
    {
        [Key]
        [ForeignKey("Employee")]
        public int Id { get; set; }
        public string password { get; set; }
        
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}
