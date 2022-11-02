using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Division> Divisions { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
