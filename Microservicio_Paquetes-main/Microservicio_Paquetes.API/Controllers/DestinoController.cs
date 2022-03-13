using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservicio_Paquetes.Domain.Entities;
using Microservicio_Paquetes.Domain.Commands;
using Microservicio_Paquetes.Domain.DTO;
using Microservicio_Paquetes.Application.Services;
using Microservicio_Paquetes.Domain.Responses;

namespace Microservicio_Paquetes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinoController : ControllerBase
    {
        private readonly IDestinoService _destinoservice;

        public DestinoController(IDestinoService destinoservice)
        {
            _destinoservice = destinoservice;
        }

       [HttpPost]
       public async Task<ActionResult> PostDestino(DestinoDto destino)
       {
            Response respuesta = _destinoservice.PostDestino(destino);

            if (respuesta.Code.Equals("BAD_REQUEST"))
            {
                return BadRequest(respuesta);
            }

            return Created("PostDestino", respuesta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDestino(int id)
        {
            object respuesta = _destinoservice.GetDestinoId(id);

            if (respuesta is Response)
            {
                if (((Response)respuesta).Code == "NOT_FOUND")
                {
                    return NotFound(respuesta);
                }
            }

            return Ok(respuesta);
        }

        [HttpGet]
        public async Task<ActionResult> GetDestinos()
        {
            object respuesta = _destinoservice.GetDestinos();

            if (respuesta is Response)
            {
                if (((Response)respuesta).Code == "NOT_FOUND")
                {
                    return NotFound(respuesta);
                }
            }

            return Ok(respuesta);
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarDestino(int Id, DestinoDto destino)
        {
            Response respuesta = _destinoservice.ActualizarDestino(Id, destino);

            if (respuesta.Code.Equals("BAD_REQUEST"))
            {
                return BadRequest(respuesta);
            }
            if (respuesta.Code.Equals("NOT_FOUND"))
            {
                return NotFound(respuesta);
            }

            return Ok(respuesta);
        }

        [HttpDelete]
        public async Task<ActionResult> BorrarDestino(int Id)
        {
            object respuesta = _destinoservice.BorrarDestino(Id);

            if (respuesta is Response)
            {
                if (((Response)respuesta).Code == "NOT_FOUND")
                {
                    return NotFound(respuesta);
                }
            }

            return Ok(respuesta);
        }

    }
}
