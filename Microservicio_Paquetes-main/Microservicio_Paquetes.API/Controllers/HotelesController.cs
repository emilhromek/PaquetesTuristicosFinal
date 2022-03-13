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
    public class HotelesController : ControllerBase
    {
        private readonly IHotelService _hotelservice;

        public HotelesController(IHotelService hotelservice)
        {
            _hotelservice = hotelservice;
        }

        [HttpPost]
        public async Task<ActionResult> PostHotel(HotelDto hotel)
        {
            Response respuesta = _hotelservice.PostHotel(hotel);

            if (respuesta.Code.Equals("BAD_REQUEST"))
            {
                return BadRequest(respuesta);
            }

            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetHotel(int id)
        {
            object respuesta = _hotelservice.GetHotelId(id);

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
        public async Task<ActionResult> GetHoteles([FromQuery] string idDestino = "")
        {
            object respuesta = _hotelservice.GetHoteles(idDestino);

            if (respuesta is Response)
            {
                if (((Response)respuesta).Code == "BAD_REQUEST")
                {
                    return BadRequest(respuesta);
                }

                if (((Response)respuesta).Code == "NOT_FOUND")
                {
                    return NotFound(respuesta);
                }
            }

            return Ok(respuesta);
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarHotel(int id, HotelDto hotel)
        {
            Response respuesta = _hotelservice.ActualizarHotel(id, hotel);

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

        [HttpDelete]
        public async Task<ActionResult> BorrarHotel(int id)
        {
            object respuesta = _hotelservice.BorrarHotel(id);

            if (respuesta is Response)
            {
                if (((Response)respuesta).Code == "NOT_FOUND")
                {
                    return NotFound(respuesta);
                }
            }

            return Ok(respuesta);
        }


    }
}
