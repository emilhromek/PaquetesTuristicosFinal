using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicio_Paquetes.Domain.DTO
{
    public class ComentarioDestinoDto
    {
         public string Mensaje { get; set; }
         public int DestinoId { get; set; }
         public int PasajeroId { get; set; }
    }
}
