using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turismo.Template.Application.Services;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.API.Controllers
{
    [Route("api/Pasajero")]
    [ApiController]
    public class PasajeroControllers : ControllerBase
    {
        private readonly IPasajeroServices _service;
        public PasajeroControllers(IPasajeroServices service)
        {
            _service = service;
        }

        // GET: Pasajeros
        [HttpGet]
        [ProducesResponseType(typeof(List<UserByEmailDto>), StatusCodes.Status200OK)]

        public IActionResult Get([FromQuery] string email)
        {
            try
            {
                return new JsonResult(_service.GetPasajeroByEmail(email)) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        // POST: Cargar Pasajero
        [HttpPost]
        public IActionResult Post(PasajeroDto roll)
        {
            try
            {
                return new JsonResult(_service.Create(roll)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            try
            {
                return new JsonResult(_service.getId(id)) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteId(int id)
        {
            try
            {
                var user = _service.getId(id);
                if (user != null)
                {
                    _service.deleteId(id);
                    return new JsonResult(user) { StatusCode = 200 };
                }
                else
                {
                    return new JsonResult(user) { StatusCode = 404 };
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, PasajeroDto pasajero)
        {
            try
            {
                return new JsonResult(_service.Update(id, pasajero)) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }
    }
}
