using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turismo.Template.AccessData;
using Turismo.Template.Domain.DTO.CoordinadorDTO;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Services;

namespace Turismo.Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinadoresController : ControllerBase
    {
        private readonly ICoordinadorService _services;

        public CoordinadoresController(ICoordinadorService context)
        {
            _services = context;
        }

        // GET: api/Coordinadors
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

        // GET: api/Coordinadors/5
        [HttpGet("{id}")]
        public IActionResult GetCoordinadorById(int id)
        {
            try
            {
                return new JsonResult(this._services.GetCoordinadorById(id)) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE: api/Grupos/5
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _services.DeleteById<Coordinador>(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Coordinadors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCoordinador(int id, Coordinador coordinador)
        //{
        //    if (id != coordinador.CoordinadorId)
        //    {
        //        return BadRequest();
        //    }

        //    _services.Entry(coordinador).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CoordinadorExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Coordinadors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult Coordinador(CoordinadorDTO coordinadorDTO)
        {
            try
            {
                return new JsonResult(_services.AddCoordinador(coordinadorDTO)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut]
        public IActionResult ActualizarCoordinador(int id, CoordinadorDTO coordinadorDTO)
        {
            try
            {
                return new JsonResult(_services.ActualizarCoordinador(id, coordinadorDTO)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("agregarAgenda")]
        [HttpPost]
        public IActionResult agregarAgenda(AgendaCoordinadorDTO agendaDTO)
        {
            try
            {
                new JsonResult(_services.agregarAgenda(agendaDTO));
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("retornarAgenda")]
        [HttpGet]
        public IActionResult retornarAgenda(int CoordinadorId)
        {
            try
            {
                return new JsonResult(_services.retornarAgenda(CoordinadorId)) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("agenda/CheckSiFechaEstaOcupada")]
        [HttpGet]
        public IActionResult checkSiFechaEstaOcupada(int CoordinadorId, DateTime fecha)
        {
            try
            {
                new JsonResult(_services.checkSiFechaEstaOcupada(CoordinadorId, fecha));
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("retornarAgendaDeEsosDias")]
        [HttpGet]
        public IActionResult retornarAgendaDeEsosDias(int CoordinadorId, DateTime fecha)
        {
            try
            {
                new JsonResult(_services.retornarAgendaDeEsosDias(CoordinadorId, fecha));
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("vaciarAgenda")]
        [HttpDelete]
        public IActionResult vaciarAgendaDeCoordinador(int CoordinadorId)
        {
            try
            {
                _services.vaciarAgendaDeCoordinador(CoordinadorId);
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("borrarAgendaSegunViaje")]
        [HttpDelete]
        public IActionResult eliminarAgendaSegunViajeId(int CoordinadorId, int viajeId)
        {
            try
            {
                _services.eliminarAgendaSegunViajeId(CoordinadorId, viajeId);
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //[HttpGet("Completo")]
        //public IActionResult GetCompleto()
        //{
        //    try
        //    {
        //        return new JsonResult(_services.GetCompleto()) { StatusCode = 200 };
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        // DELETE: api/Coordinadors/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Coordinador>> DeleteCoordinador(int id)
        //{
        //    var coordinador = await _services.Coordinadors.FindAsync(id);
        //    if (coordinador == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Coordinadors.Remove(coordinador);
        //    await _context.SaveChangesAsync();

        //    return coordinador;
        //}

        //private bool CoordinadorExists(int id)
        //{
        //    return _context.Coordinadors.Any(e => e.CoordinadorId == id);
        //}
    }
}
