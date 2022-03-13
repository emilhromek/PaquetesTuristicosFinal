using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicio_Paquetes.Domain.DTO
{
    public class ComentarioDto
    {
         public string Mensaje { get; set; }
         public int PaqueteId { get; set; }
         public int PasajeroId { get; set; }
    }
}
