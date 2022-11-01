using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using WebApp.Context;
using WebApp.Handlers;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        MyContext myContext;

        public AccountController(MyContext myContext) 
        {
            this.myContext = myContext;
        }
        
        //public IActionResult Index(string email, string password)
        //{
        //    //LoginVM login = new LoginVM("timothyhutson@gmail.com", "password");

        //    var myUser = (from us in myContext.Users 
        //                  join em in myContext.Employees on us.Id equals em.Id
        //                  join ro in myContext.Roles     on us.RoleId equals ro.Id
        //                  where (em.Email == email && us.password == password)
        //                  select new ResponseLogin()
        //                  {
        //                      FullName = em.FullName,
        //                      Email = em.Email,
        //                      Role = ro.Name
        //                  }).SingleOrDefault();

        //    if (myUser != null)
        //        return RedirectToAction("Index", "Home", new ResponseLogin() { FullName =  myUser.FullName, Email = myUser.Email, Role = myUser.Role }); 
            
        //    return View();
        //}

        //Login
        
        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var data = myContext.Users
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .SingleOrDefault(x => x.Employee.Email.Equals(email));
            var validate = Hashing.ValidatePassword(password, data.password);

            if (data != null && validate)
            {                
                HttpContext.Session.SetInt32("Id", data.Id);
                HttpContext.Session.SetString("Fullname", data.Employee.FullName);
                HttpContext.Session.SetString("Email", data.Employee.Email);
                HttpContext.Session.SetString("Role", data.Role.Name);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //Register
        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullname, string email, DateTime birthDate, string password)
        {
            var checkEmail = myContext.Employees.Any(x => x.Email.Equals(email));
            if (checkEmail)
                return View();

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
                    password = Hashing.HashPassword(password),
                    RoleId = 1
                };
                myContext.Users.Add(user);
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                    return RedirectToAction("Login", "Account");
            }
            
            return View();
        }

        //Change Password
        public IActionResult ChangePass() 
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult ChangePass(/*string email, */string currentPass, string newPass, string confirmPass)
        {
            if (confirmPass == newPass)
            {
                var data = myContext.Users
                .Include(x => x.Employee)
                .SingleOrDefault(x => x.Employee.Email.Equals(HttpContext.Session.GetString("Email")));                
                var validate = Hashing.ValidatePassword(currentPass, data.password);
                if (data != null && validate)
                {
                    data.password = Hashing.HashPassword(newPass);
                    myContext.Entry(data).State = EntityState.Modified;
                    var result = myContext.SaveChanges();
                    if (result > 0)
                        return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }


        //Forgot Password
        public IActionResult ForgotPass() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPass(string email)
        {
            var data = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email));
            if (data != null)
            {
                Employee employeeEmail = new Employee() { Email = data.Email };
                return RedirectToAction("ResetPass", "Account", employeeEmail);
            }
            return View();
        }

        //Page Forgot Password -> Page Reset Password
        public IActionResult ResetPass(Employee employeeEmail) 
        {
            return View(employeeEmail);
        }

        [HttpPost]
        public IActionResult ResetPass(string email, string newPass, string confirmPass)
        {
            if (confirmPass == newPass)
            {
                var data = myContext.Users
                .Include(x => x.Employee)
                .SingleOrDefault(x => x.Employee.Email.Equals(email));
                if (data != null)
                {
                    data.password = Hashing.HashPassword(newPass);
                    myContext.Entry(data).State = EntityState.Modified;
                    var result = myContext.SaveChanges();
                    if (result > 0)
                        return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }

    }
}
