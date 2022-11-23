using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class EmployeeRepository
    {
        private MyContext myContext;
        public EmployeeRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<Employee> Get()
        {
            return myContext.Set<Employee>().ToList();
        }
    }
}
