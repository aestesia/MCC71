using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Department
    {
        public Department(int Id, string Name, int DivisionId)
        {
            this.Id = Id;
            this.Name = Name;
            this.DivisionId = DivisionId;
        }

        public Department() 
        {
        
        }
        
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        [ForeignKey("division")]
        public int DivisionId { get; set; }
        public virtual Division Division { get; set; }

    }
}
