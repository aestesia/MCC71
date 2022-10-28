using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC71.Models
{
    public class Division
    {

        public Division(string Name)
        {
            this.Name = Name;
        }       

        public Division(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    
    }
}

