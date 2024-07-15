﻿using BL;
using Microsoft.AspNetCore.Mvc;
using DL;

namespace WS_INNOTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = CompraService.GetAll();
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
            var result = CompraService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Object);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }      
        
        [HttpGet("GetByUserId")]
        public IActionResult GetByUsaerId(int id)
        {
            var result = CompraService.GetByUserId(id);
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
        public IActionResult Post (int idUsuario, int idProducto)
        {
            var result = CompraService.Insert(idUsuario,idProducto);
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
        public IActionResult Put(int id, [FromBody] Compra compra)
        {
            var result = CompraService.Update(id, compra);
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
            var result = CompraService.Delete(id);
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
