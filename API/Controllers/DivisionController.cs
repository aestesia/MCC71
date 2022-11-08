using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
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
        public ActionResult GetAll() 
        {
            try
            {
                var data = divisionRepository.GetAll();
                return Ok(data);
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
                var data = divisionRepository.GetById(id);
                if (data == null)
                    return Ok(new { Message = "Data not found" });
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Create(Division division) 
        {
            try
            {
                var result = divisionRepository.Create(division);
                if (result == 0)
                    return Ok(new { Message = "Failed to Create Data" });
                return Ok(new { Message = "Insert Data Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        [HttpPut]
        public ActionResult Update(Division division) 
        {
            try
            {
                var result = divisionRepository.Update(division);
                if (result == 0)
                    return Ok(new { Message = "Failed to Update Data" });
                return Ok(new { Message = "Update Data Success" });
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
                var result = divisionRepository.Delete(id);
                if (result == 0)
                    return Ok(new { Message = "Failed to Delete Data" });
                return Ok(new { Message = "Delete Data Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

    }
}
