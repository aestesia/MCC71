using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC71.Models
{
    public class Department
    {
        public Department(int Id, string Nama, int DivisionId)
        {
            this.Id = Id;
            this.Nama = Nama;
            this.DivisionId = DivisionId;
        }
        public Department(int Id, string Nama)
        {
            this.Id = Id;
            this.Nama = Nama;
        }

        public Department(string Nama, int DivisionId)
        {
            this.Nama = Nama;
            this.DivisionId = DivisionId;
        }

        public int Id { get; private set; }
        public string Nama { get; private set; }
        public int DivisionId { get; private set; }
    }
}
