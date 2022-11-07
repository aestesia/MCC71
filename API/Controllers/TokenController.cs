//using API.Context;
//using API.Handlers;
//using API.Models;
//using API.ViewModels;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;

//namespace API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TokenController : ControllerBase
//    {
//        public IConfiguration configuration;
//        private readonly MyContext myContext;

//        public TokenController(IConfiguration configuration, MyContext myContext) 
//        {
//            this.configuration = configuration;
//            this.myContext = myContext;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Post(string email/*, string password*/) 
//        {
//            if(email != null /*&& password != null*/) 
//            {
//                var user = await GetUser(email/*, password*/);
//                if (user != null) 
//                {
//                    //create claims details based on the user information
//                    var claims = new[] {
//                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
//                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
//                        new Claim("Id", user.Id.ToString()),
//                        new Claim("FullName", user.FullName),
//                        new Claim("Email", user.Email),
//                        //new Claim("Role", user.Role.Name)
//                    };

//                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
//                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//                    var token = new JwtSecurityToken(
//                        configuration["Jwt:Issuer"],
//                        configuration["Jwt:Audience"],
//                        claims,
//                        expires: DateTime.UtcNow.AddMinutes(15),
//                        signingCredentials: signIn);

//                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
//                }

//                return BadRequest("Invalid Credentials");
//            }
//            return BadRequest();

//        }

//        //private async Task<ResponseLogin> GetUser(string email, string password)
//        //{
//        //    return await GetVM(email, password);
            
//        //    //myContext.Employees.FirstOrDefaultAsync(u => u.Email == email);
            
//        //    //myContext.Users.Include(x => x.Employee).Include(x => x.Role).FirstOrDefaultAsync(u => u.Employee.Email == email /*&& Hashing.ValidatePassword(password, u.Password)*/);
            
//        //}

//        //private ResponseLogin GetVM(string email, string password) 
//        //{
//        //    var data = myContext.Users.Include(x => x.Employee).Include(x => x.Role).FirstOrDefault(u => u.Employee.Email == email );
//        //    var validate = Hashing.ValidatePassword(password, data.Password);
//        //    if (data != null && validate)
//        //    {
//        //        ResponseLogin login = new ResponseLogin()
//        //        {
//        //            Id = data.Id,
//        //            FullName = data.Employee.FullName,
//        //            Email = data.Employee.Email,
//        //            Role = data.Role.Name
//        //        };

//        //        return login;
//        //    }
//        //    return null;
//        //}

//    }
//}
