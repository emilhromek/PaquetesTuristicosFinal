using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicio_Paquetes.Domain.DTO
{
    public class ComentarioDestinoOutDto
    {
         public int Id { get; set; }
         public string Fecha { get; set; }
         public string Mensaje { get; set; }
         public int DestinoId { get; set; }
         public int PasajeroId { get; set; }
        public string NombreApellido { get; set; }
    }
}
