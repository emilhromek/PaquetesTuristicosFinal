using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turismo.Template.Domain.DTO.TerminalDTO;
using Turismo.Template.Domain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Turismo.Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminalController : ControllerBase
    {
        private readonly ITerminalService _services;

        public TerminalController(ITerminalService context)
        {
            _services = context;
        }

        [HttpPost]
        public IActionResult Terminal(TerminalDTO terminalDTO)
        {
            try
            {
                return new JsonResult(_services.AddTerminal(terminalDTO)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //MODIFICADO
        [HttpGet("{id}")]
        public IActionResult GetTerminalById(int id)
        {
            try
            {
                return new JsonResult(this._services.GetTerminalById(id)) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult ActualizarTerminal(int id, TerminalDTO terminalDTO)
        {
            try
            {
                return new JsonResult(_services.ActualizarTerminal(id, terminalDTO)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _services.DeleteTerminalById(id);
                return Ok("Ok");
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
    }
}
