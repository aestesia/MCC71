using Microsoft.AspNetCore.Mvc;

namespace CLIENT.Controllers
{
    public class AccountController : Controller
    {
        //Login
        public IActionResult Login() 
        {
            return View();
        }

        //Register
        public IActionResult Register() 
        {
            return View();
        }

        //Change Password
        public IActionResult ChangePass() 
        {
            return View(); 
        }        

        //Forgot Password
        public IActionResult ForgotPass() 
        {
            return View();
        }                  

    }
}
