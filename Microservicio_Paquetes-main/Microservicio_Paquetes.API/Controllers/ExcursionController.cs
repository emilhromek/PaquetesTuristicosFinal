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
    public class ExcursionController : ControllerBase
    {
        private readonly IExcursionService _excursionservice;

        public ExcursionController(IExcursionService excursionservice)
        {
            _excursionservice = excursionservice;
        }

        [HttpPost]
        public async Task<ActionResult<Excursion>> PostExcursion(ExcursionDto excursion)
        {
            Response respuesta = _excursionservice.PostExcursion(excursion);

            if (respuesta.Code.Equals("BAD_REQUEST"))
            {
                return BadRequest(respuesta);
            }

            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetExcursion(int id)
        {
            object respuesta = _excursionservice.GetExcursionId(id);

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
        public async Task<ActionResult> GetExcursiones([FromQuery] string idDestino = "")
        {
            object respuesta = _excursionservice.GetExcursiones(idDestino);

            if (respuesta is Response)
            {
                if (((Response)respuesta).Code == "BAD_REQUEST")
                {
                    return BadRequest(respuesta);
                }

                if (((Response)respuesta).Code == "NOT_FOUND")
                {
                    return NotFound(respuesta);
                }
            }

            return Ok(respuesta);
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarExcursiones(int id, ExcursionDto excursion)
        {
            Response respuesta = _excursionservice.ActualizarExcursion(id, excursion);

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
        public async Task<ActionResult> BorrarExcursion(int id)
        {
            object respuesta = _excursionservice.BorrarExcursion(id);

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
