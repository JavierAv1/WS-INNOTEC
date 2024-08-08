using BL;
using Microsoft.AspNetCore.Mvc;
using DL;

namespace WS_INNOTEC.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        [HttpGet("GetAllDepto")]
        public IActionResult GetAll()
        {
            var result = DepartamentoService.GetAll();
            if (result.Success)
            {
                return Ok(result.Results);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet( "GetMenu")]
        public IActionResult GetMenuItems()
        {
            var result = DepartamentoService.GetMenuItems();
            if (result.Success)
            {
                return Ok(result.Results);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet("GetByIdDepto")]
        public IActionResult GetById(int id)
        {
            var result = DepartamentoService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Object);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost("InsertDepto")]
        public IActionResult Post([FromBody] Departamento departamento)
        {
            var result = DepartamentoService.Insert(departamento);
            if (result.Success)
            {
                return Ok(result.Object);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPut("UpdateDepto")]
        public IActionResult Put(int id, [FromBody] Departamento departamento)
        {
            var result = DepartamentoService.Update(id,departamento);
            if (result.Success)
            {
                return Ok(result.Object);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpDelete("DeleteDepto")]
        public IActionResult Delete(int id)
        {
            var result = DepartamentoService.Delete(id);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

    }
}
