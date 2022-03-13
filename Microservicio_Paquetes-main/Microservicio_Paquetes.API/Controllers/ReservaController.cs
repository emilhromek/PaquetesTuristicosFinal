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
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _reservaservice;

        public ReservaController(IReservaService reservaservice)
        {
            _reservaservice = reservaservice;
        }

        [HttpPost]
        public async Task<ActionResult> PostReserva(ReservaDto reserva)
        {
            Response respuesta = _reservaservice.PostReserva(reserva);

            if (respuesta.Code.Equals("BAD_REQUEST"))
            {
                return BadRequest(respuesta);
            }

            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetReserva(int id)
        {
            object respuesta = _reservaservice.GetReservaId(id);

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
        public async Task<ActionResult> GetReservas(string PaqueteId, string PasajeroId)
        {
            object respuesta = _reservaservice.GetReservas(PaqueteId, PasajeroId);

            if (respuesta is Response)
            {
                if (((Response)respuesta).Code == "NOT_FOUND")
                {
                    return NotFound(respuesta);
                }
            }

            return Ok(respuesta);
        }

        [Route("pagado")]
        [HttpPatch]
        public async Task<ActionResult> PatchReservaPagado(int id)
        {
            Response respuesta = _reservaservice.PatchReservaPagadoId(id);

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

        [Route("asignarGrupo")]
        [HttpPatch]
        public async Task<ActionResult> PatchReservaGrupo(int id, int grupoId)
        {
            Response respuesta = _reservaservice.PatchReservaGrupoId(id, grupoId);

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


        [Route("desasignarGrupo")]
        [HttpPatch]
        public async Task<ActionResult> UnpatchReservaGrupo(int id)
        {
            Response respuesta = _reservaservice.UnpatchReservaGrupoId(id);

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

        [Route("asignarViaje")]
        [HttpPatch]
        public async Task<ActionResult> PatchReservaViaje(int id, int viajeId)
        {
            Response respuesta = _reservaservice.PatchReservaGrupoId(id, viajeId);

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


        [Route("desasignarViaje")]
        [HttpPatch]
        public async Task<ActionResult> UnpatchReservaViaje(int id)
        {
            Response respuesta = _reservaservice.UnpatchReservaViajeId(id);

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

        [Route("asignarViajeSegunGrupo")]
        [HttpPatch]
        public async Task<ActionResult> AsignarViajeAReservasSegunGrupo(int grupoId, int viajeId)
        {
            Response respuesta = _reservaservice.AsignarViajeAReservasSegunGrupo(grupoId, viajeId);

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

        [Route("desasignarViajeSegunGrupo")]
        [HttpPatch]
        public async Task<ActionResult> DesignarViajeAReservasSegunGrupo(int grupoId)
        {
            Response respuesta = _reservaservice.DesasignarViajeAReservasSegunGrupo(grupoId);

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



    }
}
