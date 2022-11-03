using API.Context;
using API.Handlers;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class AccountRepository
    {
        MyContext myContext;
        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public ResponseLogin Login(string email, string password) 
        {
            var data = myContext.Users
                    .Include(x => x.Employee)
                    .Include(x => x.Role)
                    .SingleOrDefault(x => x.Employee.Email.Equals(email));
            var validate = Hashing.ValidatePassword(password, data.Password);

            if (data != null && validate)
            {
                ResponseLogin login = new ResponseLogin()
                {
                    Id = data.Id,
                    FullName = data.Employee.FullName,
                    Email = data.Employee.Email,
                    Role = data.Role.Name
                };
                
                return login;
            }
            return null;
        }

        public int Register(string fullname, string email, DateTime birthDate, string password) 
        {
            var checkEmail = myContext.Employees.Any(x => x.Email.Equals(email));
            if (checkEmail)
                return 2;

            Employee employee = new Employee()
            {
                FullName = fullname,
                Email = email,
                BirthDate = birthDate
            };
            myContext.Employees.Add(employee);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
                User user = new User()
                {
                    Id = id,
                    Password = Hashing.HashPassword(password),
                    RoleId = 1
                };
                myContext.Users.Add(user);
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                    return resultUser;
            }
            return 0;
        }

        public int ChangePass(string email, string currentPass, string newPass, string confirmPass) 
        {
            if (confirmPass != newPass)
                return 2;

            var data = myContext.Users
                .Include(x => x.Employee)
                .SingleOrDefault(x => x.Employee.Email.Equals(email));
            var validate = Hashing.ValidatePassword(currentPass, data.Password);
            if (data != null && validate)
            {
                data.Password = Hashing.HashPassword(newPass);
                myContext.Entry(data).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return result;
            }
            return 0;
        }

        public int ForgotPass(string email, string newPass, string confirmPass) 
        {
            if (confirmPass != newPass)
                return 2;

            var data = myContext.Users
                .Include(x => x.Employee)
                .SingleOrDefault(x => x.Employee.Email.Equals(email));
            if (data != null)
            {
                data.Password = Hashing.HashPassword(newPass);
                myContext.Entry(data).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return result;

            }
            return 0;
        }

    }
}
