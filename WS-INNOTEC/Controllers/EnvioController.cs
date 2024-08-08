using BL;
using Microsoft.AspNetCore.Mvc;
using DL;

namespace WS_INNOTEC.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EnvioController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = EnvioService.GetAll();
            if (result.Success)
            {
                return Ok(result.Results);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = EnvioService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Object);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost("Insert")]
        public IActionResult Post([FromBody] Envio envio)
        {
            var result = EnvioService.Insert(envio);
            if (result.Success)
            {
                return Ok(result.Object);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPut("Update")]
        public IActionResult Put(int id, [FromBody] Envio envio)
        {
            var result = EnvioService.Update(id, envio);
            if (result.Success)
            {
                return Ok(result.Object);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var result = EnvioService.Delete(id);
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
