using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Division
    {
        public Division(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public Division()
        {

        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}

