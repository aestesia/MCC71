using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CLIENT.Controllers
{
    public class DivisionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
