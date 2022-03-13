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
using Microservicio_Paquetes.Domain.Queries;
using Microservicio_Paquetes.Domain.Responses;

namespace Microservicio_Paquetes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaquetesController : ControllerBase
    {
        private readonly IPaqueteService _paqueteservice;

        public PaquetesController(IPaqueteService paqueteservice)
        {
            _paqueteservice = paqueteservice;
        }

        [HttpPost]
        public async Task<ActionResult> PostPaquete(PaqueteDto paquete)
        {
            Response respuesta = _paqueteservice.PostPaquete(paquete);

            if (respuesta.Code.Equals("BAD_REQUEST"))
            {
                return BadRequest(respuesta);
            }

            return Ok(respuesta);
        }

        [HttpGet]
        public async Task<ActionResult> GetPaquetes([FromQuery] string idDestino = "")
        {
            object respuesta = _paqueteservice.GetPaquetes(idDestino);

            if (respuesta is Response)
            {
                if (((Response)respuesta).Code == "NOT_FOUND")
                {
                    return NotFound(respuesta);
                }
            }

            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPaquete(int id)
        {
            object respuesta = _paqueteservice.GetPaqueteId(id);

            if (respuesta is Response)
            {
                if (((Response)respuesta).Code == "NOT_FOUND")
                {
                    return NotFound(respuesta);
                }
            }

            return Ok(respuesta);
        }

        [HttpDelete]
        public async Task<ActionResult> BorrarPaquete(int id)
        {
            object respuesta = _paqueteservice.deletePaqueteId(id);

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
