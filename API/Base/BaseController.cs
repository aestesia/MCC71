using API.Models;
using API.Repositories.Data;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class BaseController<Repository, Entity, Key> : ControllerBase
        where Repository : class, IRepository<Entity, Key>
        where Entity : class
    {
        Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            try
            {
                var data = repository.GetAll();
                if (data == null)
                    return Ok(new { StatusCode = 200, Message = "Data not found" });
                return Ok(new { StatusCode = 200, Message = "Data Found", Data = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public virtual IActionResult GetById(Key id) 
        {
            try
            {
                var data = repository.GetById(id);
                if (data == null)
                    return Ok(new { StatusCode = 200, Message = "Data not found" });
                return Ok(new { StatusCode = 200, Message = "Data found", Data = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(Entity entity) 
        {
            try
            {
                var result = repository.Create(entity);
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
        public IActionResult Update(Entity entity) 
        {
            try
            {
                var result = repository.Update(entity);
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
        public IActionResult Delete(Key id) 
        {
            try
            {
                var result = repository.Delete(id);
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
