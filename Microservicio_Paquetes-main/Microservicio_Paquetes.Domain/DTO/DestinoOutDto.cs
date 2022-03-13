using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicio_Paquetes.Domain.DTO
{
    public class DestinoOutDto
    {
        public int Id { get; set; }
        public string Lugar { get; set; }
        public string Descripcion { get; set; }
        public string Atractivo { get; set; }
        public string Historia { get; set; }
        public int HorasDeViaje { get; set; }
        public string Imagen { get; set; }
    }
}
