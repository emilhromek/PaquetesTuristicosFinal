using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turismo.Template.Domain.DTO.Chofer;
using Turismo.Template.Domain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Turismo.Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoferController : ControllerBase
    {
        private readonly IChoferService _services;

        public ChoferController(IChoferService context)
        {
            _services = context;
        }

        [HttpPost]
        public IActionResult Chofer(ChoferDTO choferDTO)
        {
            try
            {
                return new JsonResult(_services.AddChofer(choferDTO)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetChoferById(int id)
        {
            try
            {
                return new JsonResult(this._services.GetChoferById(id)) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _services.DeleteChoferById(id);
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult ActualizarChofer(int id, ChoferDTO choferDTO)
        {
            try
            {
                return new JsonResult(_services.ActualizarChofer(id, choferDTO)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return new JsonResult(_services.GetAll()) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("queEstenLibres")]
        [HttpGet]
        public IActionResult GetAllQueEstenLibres(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                return new JsonResult(_services.GetAllQueEstenLibres(fechaInicial, fechaFinal)) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("agregarAgenda")]
        [HttpPost]
        public IActionResult agregarAgenda(AgendaChoferDTO agendaDTO)
        {
            try
            {
                return new JsonResult(_services.agregarAgenda(agendaDTO)) { StatusCode = 201 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("retornarAgenda")]
        [HttpGet]
        public IActionResult retornarAgenda(int ChoferId)
        {
            try
            {
                return new JsonResult(_services.retornarAgenda(ChoferId)) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("agenda/CheckSiFechaEstaOcupada")]
        [HttpGet]
        public IActionResult checkSiFechaEstaOcupada(int ChoferId, DateTime fecha)
        {
            try
            {
                return new JsonResult(_services.checkSiFechaEstaOcupada(ChoferId, fecha)) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("retornarAgendaDeEsosDias")]
        [HttpGet]
        public IActionResult retornarAgendaDeEsosDias(int ChoferId, DateTime fecha)
        {
            try
            {
                return new JsonResult(_services.retornarAgendaDeEsosDias(ChoferId, fecha)) { StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("vaciarAgenda")]
        [HttpDelete]
        public IActionResult vaciarAgendaDeBus(int ChoferId)
        {
            try
            {
                _services.vaciarAgendaDeChofer(ChoferId);
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("borrarAgendaSegunViaje")]
        [HttpDelete]
        public IActionResult eliminarAgendaSegunViajeId(int ChoferId, int viajeId)
        {
            try
            {
                _services.eliminarAgendaSegunViajeId(ChoferId, viajeId);
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
