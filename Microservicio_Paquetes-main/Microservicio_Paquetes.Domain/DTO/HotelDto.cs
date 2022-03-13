using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicio_Paquetes.Domain.DTO
{
    public class HotelDto
    {
        public string Marca { get; set; }
        public string Sucursal { get; set; }
        public int Estrellas { get; set; }
        public string Direccion { get; set; }
        public int DestinoId { get; set; }
        public int Capacidad { get; set; }
        public int Costo { get; set; }
    }
}
