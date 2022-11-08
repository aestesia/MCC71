using API.Context;
using API.Handlers;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IConfiguration configuration;
        private AccountRepository accountRepository;

        public AccountController(IConfiguration configuration, AccountRepository accountRepository)
        {
            this.configuration = configuration;
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

                //create claims details based on the user information
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("id", login.Id.ToString()),
                        new Claim("fullName", login.FullName),
                        new Claim("email", login.Email),
                        new Claim("role", login.Role)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: signIn);

                return Ok(new { StatusCode = 200, Message = "Login Success", Data = new JwtSecurityTokenHandler().WriteToken(token) });
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
