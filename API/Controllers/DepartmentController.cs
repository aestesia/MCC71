using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private DepartmentRepository departmentRepository;

        public DepartmentController(DepartmentRepository departmentRepository) 
        {
            this.departmentRepository = departmentRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var data = departmentRepository.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var data = departmentRepository.GetById(id);
            if (data == null)
                return Ok(new { Message = "Data not found" });
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            var result = departmentRepository.Create(department);
            if (result == 0)
                return Ok(new { Message = "Failed to Create Data" });
            return Ok(new { Message = "Insert Data Success" });
        }

        [HttpPut]
        public ActionResult Update(Department department)
        {
            var result = departmentRepository.Update(department);
            if (result == 0)
                return Ok(new { Message = "Failed to Update Data" });
            return Ok(new { Message = "Update Data Success" });
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = departmentRepository.Delete(id);
            if (result == 0)
                return Ok(new { Message = "Failed to Delete Data" });
            return Ok(new { Message = "Delete Data Success" });
        }
    }
}
