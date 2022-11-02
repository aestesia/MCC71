using API.Context;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class DivisionRepository : IRepository<Division, int>
    {
        private MyContext myContext;
        public DivisionRepository(MyContext myContext) 
        {
            this.myContext = myContext;
        }

        //Get All
        public IEnumerable<Division> GetAll() 
        {
            return myContext.Divisions.ToList();
        }

        //Get by Id
        public Division GetById(int id) 
        {
            return myContext.Divisions.Find(id);
        }

        //Create
        public int Create(Division division) 
        {
            myContext.Divisions.Add(division);
            var result = myContext.SaveChanges();
            return result;
        }

        //Update
        public int Update(Division division) 
        {
            myContext.Entry(division).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }

        //Delete
        public int Delete(int id) 
        {
            var data = myContext.Divisions.Find(id);
            if (data != null) 
            {
                myContext.Remove(data);
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }

    }
}
