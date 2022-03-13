using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turismo.Template.Domain.DTO.Bus;
using Turismo.Template.Domain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Turismo.Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly IBusService _services;

        public BusController(IBusService context)
        {
            _services = context;
        }

        [HttpPost]
        public IActionResult Bus(BusDTO busDTO)
        {
            try
            {
                return new JsonResult(_services.AddBus(busDTO)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBusById(int id)
        {
            try
            {
                var bus = this._services.GetBusById(id);
                if (bus == null)
                    throw new Exception($"El viaje con id={id} no existe");
                return new JsonResult(bus) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult ActualizarBus(int id, BusDTO busDTO)
        {
            try
            {
                return new JsonResult(_services.ActualizarBus(id, busDTO)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //[HttpGet("{Id}")]
        //public IActionResult GetBusById(int id)
        //{
        //    try
        //    {
        //        return new JsonResult(this._services.GetBusById(id)) { StatusCode = 200 };
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _services.DeleteBusById(id);
                return Ok("Borrado ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        public IActionResult agregarAgenda(AgendaBusDTO agendaDTO)
        {
            try
            {
                new JsonResult(_services.agregarAgenda(agendaDTO));
                return Ok("Ok");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("retornarAgenda")]
        [HttpGet]
        public IActionResult retornarAgenda(int BusId)
        {
            try
            {
                return new JsonResult(_services.retornarAgenda(BusId)) { StatusCode = 200 };
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("retornarAgendaConFormato")]
        [HttpGet]
        public IActionResult retornarAgendaConFormato(int BusId)
        {
            try
            {
                return new JsonResult(_services.retornarAgendaConFormato(BusId)) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("agenda/CheckSiFechaEstaOcupada")]
        [HttpGet]
        public IActionResult checkSiFechaEstaOcupada(int BusId, DateTime fecha)
        {
            try
            {
                new JsonResult(_services.checkSiFechaEstaOcupada(BusId, fecha));
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("retornarAgendaDeEsosDias")]
        [HttpGet]
        public IActionResult retornarAgendaDeEsosDias(int BusId, DateTime fecha)
        {
            try
            {
                new JsonResult(_services.retornarAgendaDeEsosDias(BusId, fecha));
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("vaciarAgenda")]
        [HttpDelete]
        public IActionResult vaciarAgendaDeBus(int BusId)
        {
            try
            {
                _services.vaciarAgendaDeBus(BusId);
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("borrarAgendaSegunViaje")]
        [HttpDelete]
        public IActionResult eliminarAgendaSegunViajeId(int BusId, int viajeId)
        {
            try
            {
                _services.eliminarAgendaSegunViajeId(BusId, viajeId);
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
