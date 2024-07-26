using BL;
using Microsoft.AspNetCore.Mvc;

namespace WS_INNOTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        DL.Result result = new DL.Result();

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            result = ProductoService.GetAll();
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
            result = ProductoService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Object);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string product)
        {
            result = ProductoService.GetByName(product);
            if (result.Success)
            {
                return Ok(result.Results);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost("Insert")]
        public IActionResult Post([FromBody] DL.Producto productosML)
        {

            result = ProductoService.Insert(productosML);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost("Update")]
        public IActionResult Put(int id, [FromBody] DL.Producto productoML)
        {
            if (id != productoML.IdProductos)
            {
                return BadRequest("Product ID mismatch.");
            }

            result = ProductoService.Update(id, productoML);
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
            result = ProductoService.Delete(id);
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
