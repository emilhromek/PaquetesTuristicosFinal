using Microsoft.AspNetCore.Cors;
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
    [Route("api/Roll")]
    [ApiController]
    public class RollControllers : ControllerBase
    {
        private readonly IRollServices _service;
        public RollControllers(IRollServices service)
        {
            _service = service;
        }

        // GET: Roles
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return new JsonResult(_service.getAll()) { StatusCode = 200 };
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

    }
}
