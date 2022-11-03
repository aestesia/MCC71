using API.Context;
using API.Handlers;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccountRepository accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        //LOGIN
        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                ResponseLogin login = accountRepository.Login(email, password);
                if (login == null)
                    return Ok(new { StatusCode = 200, Message = "Login Failed" });
                return Ok(new { StatusCode = 200, Message = "Login Success", Data = login});
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
                var result = accountRepository.Register(fullname, email, birthDate, password);
                if(result == 2)
                    return Ok(new { StatusCode = 200, Message = "Email is Already Used" });
                if (result == 0)
                    return Ok(new { StatusCode = 200, Message = "Failed To Register" });
                return Ok(new { StatusCode = 200, Message = "Register Success" });
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
                var result = accountRepository.ChangePass(email, currentPass, newPass, confirmPass);
                if (result == 2)
                    return Ok(new { StatusCode = 200, Message = "Password and Confirm Password are Mismatch" });
                if (result == 0)
                    return Ok(new { StatusCode = 200, Message = "Failed To Change Password" });
                return Ok(new { StatusCode = 200, Message = "Change Password Success" });
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
                var result = accountRepository.ForgotPass(email, newPass, confirmPass);
                if (result == 2)
                    return Ok(new { StatusCode = 200, Message = "Password and Confirm Password are Mismatch" });
                if (result == 0)
                    return Ok(new { StatusCode = 200, Message = "Failed To Reset Password" });
                return Ok(new { StatusCode = 200, Message = "Reset Password Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

    }
}
