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
    public class ComentarioDestinoController : ControllerBase
    {
        private readonly IComentarioDestinoService _comentarioservice;

        public ComentarioDestinoController(IComentarioDestinoService comentarioservice)
        {
            _comentarioservice = comentarioservice;
        }

        [HttpPost]
        public async Task<ActionResult> PostComentarioDestino(ComentarioDestinoDto comentario)
        {
            Response respuesta = _comentarioservice.PostComentarioDestino(comentario);

            if (respuesta.Code.Equals("BAD_REQUEST"))
            {
                return BadRequest(respuesta);
            }

            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComentarioDestino>> GetComentarioDestino(int id)
        {
            object respuesta = _comentarioservice.GetComentarioDestinoId(id);

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
        public async Task<ActionResult> GetComentariosDestino([FromQuery] string idPasajero = "", [FromQuery] string idDestino = "")
        {
            object respuesta = _comentarioservice.GetComentariosDestino(idDestino, idPasajero);

            if (respuesta is Response)
            {
                if (((Response)respuesta).Code == "BAD_REQUEST")
                {
                    return BadRequest(respuesta);
                }

                if (((Response) respuesta).Code == "NOT_FOUND")
                {
                    return NotFound(respuesta);
                }
            }

            return Ok(respuesta);

        }
    }
}
