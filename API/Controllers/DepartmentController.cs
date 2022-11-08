using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class DepartmentController : ControllerBase
    {
        private DepartmentRepository departmentRepository;

        public DepartmentController(DepartmentRepository departmentRepository) 
        {
            this.departmentRepository = departmentRepository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var data = departmentRepository.GetAll();
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

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var data = departmentRepository.GetById(id);
                if (data == null)
                    return Ok(new { StatusCode = 200, Message = "Data not found" });
                return Ok(new { StatusCode = 200, Message = "Data not found" , Data = data});
            }
            catch (Exception ex) 
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            try
            {
                var result = departmentRepository.Create(department);
                if (result == 0)
                    return Ok(new { StatusCode = 200, Message = "Failed to Create Data" });
                return Ok(new { StatusCode = 200, Message = "Insert Data Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        [HttpPut]
        public ActionResult Update(Department department)
        {
            try
            {
                var result = departmentRepository.Update(department);
                if (result == 0)
                    return Ok(new { StatusCode = 200, Message = "Failed to Update Data" });
                return Ok(new { StatusCode = 200, Message = "Update Data Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = departmentRepository.Delete(id);
                if (result == 0)
                    return Ok(new { StatusCode = 200, Message = "Failed to Delete Data" });
                return Ok(new { StatusCode = 200, Message = "Delete Data Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }
    }
}
