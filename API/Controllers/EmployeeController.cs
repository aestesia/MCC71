using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeRepository employeeRepository;

        public EmployeeController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var data = employeeRepository.Get();
                if (data == null)
                    return Ok(new { StatusCode = 200, Message = "Data Not Found" });

                //return StatusCode(200, "Data Found");
                return Ok(new { StatusCode = 200, Message = "Data Found", Data = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

    }
}
