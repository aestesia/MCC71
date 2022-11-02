using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {
        private DivisionRepository divisionRepository;

        public DivisionController(DivisionRepository divisionRepository) 
        {
            this.divisionRepository = divisionRepository;
        }

        [HttpGet]
        public ActionResult Get() 
        {
            var data = divisionRepository.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id) 
        {
            var data = divisionRepository.GetById(id);
            if (data == null)
                return Ok(new { Message = "Data not found" });
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Create(Division division) 
        {
            var result = divisionRepository.Create(division);
            if (result == 0)
                return Ok(new { Message = "Failed to Create Data" });
            return Ok(new { Message = "Insert Data Success" });
        }

        [HttpPut]
        public ActionResult Update(Division division) 
        {
            var result = divisionRepository.Update(division);
            if (result == 0)
                return Ok(new { Message = "Failed to Update Data" });
            return Ok(new { Message = "Update Data Success" });
        }

        [HttpDelete]
        public ActionResult Delete(int id) 
        {
            var result = divisionRepository.Delete(id);
            if (result == 0)
                return Ok(new { Message = "Failed to Delete Data" });
            return Ok(new { Message = "Delete Data Success" });
        }

    }
}
