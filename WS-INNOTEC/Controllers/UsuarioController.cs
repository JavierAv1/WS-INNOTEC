using BL;
using Microsoft.AspNetCore.Mvc;
using DL;  

namespace WS_INNOTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = UsuarioService.GetAll();
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
            var result = UsuarioService.GetById(id);
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
        public IActionResult Post([FromBody] Usuario usuario)
        {
            Result result = UsuarioService.Insert(usuario);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPut("Update")]
        public IActionResult Put(int id, [FromBody] Usuario usuario)
        {

            var result = UsuarioService.Update(id,usuario);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var result = UsuarioService.Delete(id);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet("Login")]
        public IActionResult Login(string userNameOrEmail, string password)
        {
            var result = UsuarioService.Login(userNameOrEmail, password);
            if (result.Success)
            {
                return Ok(new { Success = true, Object = result.Object });
            }
            else
            {
                return Ok(new { Success = false, ErrorMessage = result.ErrorMessage });
            }
        }

    }
}
