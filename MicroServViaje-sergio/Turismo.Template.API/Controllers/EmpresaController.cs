using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turismo.Template.Domain.DTO.Empresa;
using Turismo.Template.Domain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Turismo.Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _services;

        public EmpresaController(IEmpresaService context)
        {
            _services = context;
        }

        [HttpPost]
        public IActionResult Empresa(EmpresaDTO empresaDTO)
        {
            try
            {
                return new JsonResult(_services.AddEmpresa(empresaDTO)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEmpresaById(int id)
        {
            try
            {
                return new JsonResult(this._services.GetEmpresaById(id)) { StatusCode = 200 };
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

        [HttpPut]
        public IActionResult ActualizarEmpresa(int id, EmpresaDTO empresaDTO)
        {
            try
            {
                return new JsonResult(_services.ActualizarEmpresa(id, empresaDTO)) { StatusCode = 201 };
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
                _services.DeleteEmpresaById(id);
                return Ok("ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
