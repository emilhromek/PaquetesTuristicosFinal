using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turismo.Template.AccessData;
using Turismo.Template.Domain.DTO.Grupo;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Services;

namespace Turismo.Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposController : ControllerBase
    {
        private readonly IGrupoService _services;

        public GruposController(IGrupoService context)
        {
            _services = context;
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

        [Route("porPaquete")]
        [HttpGet]
        public IActionResult GetAllPorPaquete(int PaqueteId)
        {
            try
            {
                return new JsonResult(_services.GetAllPorPaquete(PaqueteId)) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetGrupoById(int id)
        {
            try
            {
                return new JsonResult(this._services.GetGrupoById(id)) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Grupoes/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGrupo(int id, Grupo grupo)
        //{
        //    if (id != grupo.GrupoId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(grupo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GrupoExists(id))
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

        // POST: api/Grupoes
        [HttpPost]
        public IActionResult Grupo(GrupoDTO grupoDTO)
        {
            try
            {
                return new JsonResult(_services.AddGrupo(grupoDTO)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("asignarViaje")]
        [HttpPatch]
        public IActionResult PatchGrupoViaje(int id, int viajeId)
        {
            try
            {
                return new JsonResult(_services.PatchGrupoViaje(id, viajeId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("desasignarViaje")]
        [HttpPatch]
        public IActionResult UnpatchGrupoViaje(int id)
        {
            try
            {
                return new JsonResult(_services.UnpatchGrupoViaje(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //DELETE: api/Grupos/5
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _services.DeleteGrupoById(id);
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete]
        //public IActionResult Delete(CuestionarioDTO cuestionarioDto)
        //{
        //    try
        //    {
        //        service.DeleteCuestionario(cuestionarioDto);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //public IActionResult DeleteGrupo(int id)
        //{
        //    try
        //    {
        //        return new JsonResult(_services.DeleteGrupoById(id)) { StatusCode = 200 };

        //    }
        //    catch (System.Exception e)
        //    {
        //        return BadRequest(e.Message);

        //    }
        //}

        //private bool GrupoExists(int id)
        //{
        //    return _context.Grupos.Any(e => e.GrupoId == id);
        //}
    }
}
