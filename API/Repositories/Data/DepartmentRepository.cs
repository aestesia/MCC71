using API.Context;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class DepartmentRepository : IRepository<Department, int>
    {
        private MyContext myContext;
        public DepartmentRepository(MyContext myContext) 
        {
            this.myContext = myContext;
        }

        public int Create(Department Entity)
        {
            myContext.Departments.Add(Entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Delete(int id)
        {
            var data = myContext.Departments.Find(id);
            if (data != null)
            {
                myContext.Remove(data);
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }

        public IEnumerable<Department> GetAll()
        {
            return myContext.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return myContext.Departments.Find(id);
        }

        public int Update(Department Entity)
        {
            myContext.Entry(Entity).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
