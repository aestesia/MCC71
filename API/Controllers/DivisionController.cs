using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class DivisionController : BaseController<DivisionRepository, Division, int>
    {
        private DivisionRepository divisionRepository;

        public DivisionController(DivisionRepository divisionRepository) : base(divisionRepository)
        {
            this.divisionRepository = divisionRepository;
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name) 
        {
            try
            {
                var data = divisionRepository.Get(name);
                if (data == null)
                    return Ok(new { StatusCode = 200, Message = "Data not found" });
                return Ok(new { StatusCode = 200, Message = "Data found", Data = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        //[HttpGet]
        //public ActionResult GetAll() 
        //{
        //    try
        //    {
        //        var data = divisionRepository.GetAll();
        //        if (data == null)
        //            return Ok(new { StatusCode = 200, Message = "Data Not Found" });

        //        //return StatusCode(200, "Data Found");
        //        return Ok(new { StatusCode = 200, Message = "Data Found", Data = data });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { StatusCode = 400, Message = ex.Message });
        //    }
        //}

        //[HttpGet("{id}")]
        //public ActionResult GetById(int id) 
        //{
        //    try
        //    {
        //        var data = divisionRepository.GetById(id);
        //        if (data == null)
        //            return Ok(new { StatusCode = 200, Message = "Data not found" });
        //        return Ok(new { StatusCode = 200, Message = "Data found", Data = data});
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { StatusCode = 400, Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public ActionResult Create(Division division) 
        //{
        //    try
        //    {
        //        var result = divisionRepository.Create(division);
        //        if (result == 0)
        //            return Ok(new { StatusCode = 200, Message = "Failed to Create Data" });
        //        return Ok(new { StatusCode = 200, Message = "Insert Data Success" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { StatusCode = 400, Message = ex.Message });
        //    }
        //}

        //[HttpPut]
        //public ActionResult Update(Division division) 
        //{
        //    try
        //    {
        //        var result = divisionRepository.Update(division);
        //        if (result == 0)
        //            return Ok(new { StatusCode = 200, Message = "Failed to Update Data" });
        //        return Ok(new { StatusCode = 200, Message = "Update Data Success" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { StatusCode = 400, Message = ex.Message });
        //    }
        //}

        //[HttpDelete]
        //public ActionResult Delete(int id) 
        //{
        //    try
        //    {
        //        var result = divisionRepository.Delete(id);
        //        if (result == 0)
        //            return Ok(new { StatusCode = 200, Message = "Failed to Delete Data" });
        //        return Ok(new { StatusCode = 200, Message = "Delete Data Success" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { StatusCode = 400, Message = ex.Message });
        //    }
        //}

    }
}
