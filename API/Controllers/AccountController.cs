using API.Context;
using API.Handlers;
using API.Models;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private AccountRepository accountRepository;

        //public AccountController(AccountRepository accountRepository) 
        //{
        //    this.accountRepository = accountRepository;   
        //}

        MyContext myContext;

        public AccountController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        //LOGIN
        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            try
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

                    //HttpContext.Session.SetInt32("Id", data.Id);
                    //HttpContext.Session.SetString("Fullname", data.Employee.FullName);
                    //HttpContext.Session.SetString("Email", data.Employee.Email);
                    //HttpContext.Session.SetString("Role", data.Role.Name);
                    return Ok(new { StatusCode = 200, Message = "Login Success", Data = login });
                }
                return Ok(new { StatusCode = 200, Message = "Login Failed" });
            }
            catch (Exception ex) 
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        //REGISTER
        [HttpPost("Register")]
        public IActionResult Register(string fullname, string email, DateTime birthDate, string password)
        {
            try
            {
                var checkEmail = myContext.Employees.Any(x => x.Email.Equals(email));
                if (checkEmail)
                    return Ok(new { StatusCode = 200, Message = "Email is Already Used" });

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
                        return Ok(new { StatusCode = 200, Message = "Register Success" });
                }
                return Ok(new { StatusCode = 200, Message = "Failed To Register" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        //CHANGE PASS
        [HttpPost("ChangePass")]
        public IActionResult ChangePass(string email, string currentPass, string newPass, string confirmPass)
        {
            try
            {
                if (confirmPass != newPass)
                    return Ok(new { StatusCode = 200, Message = "Password and Confirm Password are Mismatch" });

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
                        return Ok(new { StatusCode = 200, Message = "Change Password Success" });
                }
                return Ok(new { StatusCode = 200, Message = "Failed To Change Password" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        //FORGOT PASS
        [HttpPost("ForgotPass")]
        public IActionResult ForgotPass(string email, string newPass, string confirmPass)
        {
            try
            {
                if (confirmPass != newPass)
                    return Ok(new { StatusCode = 200, Message = "Password and Confirm Password are Mismatch" });

                var data = myContext.Users
                    .Include(x => x.Employee)
                    .SingleOrDefault(x => x.Employee.Email.Equals(email));
                if (data != null)
                {
                    data.Password = Hashing.HashPassword(newPass);
                    myContext.Entry(data).State = EntityState.Modified;
                    var result = myContext.SaveChanges();
                    if (result > 0)
                        return Ok(new { StatusCode = 200, Message = "Reset Password Success" });

                }
                return Ok(new { StatusCode = 200, Message = "Failed To Reset Password" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

    }
}
