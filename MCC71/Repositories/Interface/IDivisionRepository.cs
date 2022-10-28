using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCC71.Models;

namespace MCC71.Repositories.Interface
{
    public interface IDivisionRepository
    {
        public List<Division> Get();
        public Division Get(int id);
        public int Insert(Division division);
        public int Update(Division division);
        public int Delete(int id);
    }
}
