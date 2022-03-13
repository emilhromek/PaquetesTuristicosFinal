using Microservicio_Paquetes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicio_Paquetes.Domain.DTO
{
    public class ReservaDto
    {
        public int Pasajeros { get; set; }
        public bool Pagado { get; set; }
        public int PasajeroId { get; set; }
        public int FormaPagoId { get; set; }
        public int PaqueteId { get; set; }
        public List<int> ListaExcursiones { get; set; }
    }
}
