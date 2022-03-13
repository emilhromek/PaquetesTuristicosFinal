using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turismo.Template.AccessData;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Services;

namespace Turismo.Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajesController : ControllerBase
    {
        private readonly IViajeServices _services;

        public ViajesController(IViajeServices context)
        {
            _services = context;
        }

        [HttpPost]
        public IActionResult PostViaje(ViajeDTO viajeDTO)
        {
            try
            {
                return new JsonResult(_services.AddViaje(viajeDTO)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetViajeById(int id)
        {
            try
            {
                return new JsonResult(this._services.GetViajeById(id)) { StatusCode = 200 };
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
                _services.DeleteById<Viaje>(id);
                return Ok();
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

        //[HttpPatch("GetViajesByLista")]
        //public async Task<IActionResult> GetViajeByLista(List<int> idViajes)
        //{
        //    try
        //    {
        //        return new JsonResult(await _services.GetViajeByIdLista(idViajes)) { StatusCode = 200 };
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


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

        [Route("porGrupo")]
        [HttpGet]
        public IActionResult GetByParamPaqueteId([FromQuery]int grupoId)
        {
            try
            {
                return new JsonResult(_services.GetBy(grupoId)) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("designaciones/{viajeId}")]
        [HttpPost]
        public IActionResult PostDesignacion(int viajeId, [FromBody] DesignacionDTO designacionDTO)
        {
            try
            {
                return new JsonResult( _services.AddDesignacion(viajeId, designacionDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


//POST     //para designar un chofer a un viaje y si quiere también un bus por Id
//api/viaje/designacionesDeViaje/{viajeId}
//{
//	chofe1: Id,
//	chofer2: Id,
//	bus: Id
//}

    }
}
