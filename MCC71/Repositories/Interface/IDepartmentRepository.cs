using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCC71.Models;

namespace MCC71.Repositories.Interface
{
    public interface IDepartmentRepository
    {
        public List<Department> Get();
        public Department Get(int id);
        public int Insert(Department department);
        public int Update(Department department);
        public int Delete(int id);
    }
}
