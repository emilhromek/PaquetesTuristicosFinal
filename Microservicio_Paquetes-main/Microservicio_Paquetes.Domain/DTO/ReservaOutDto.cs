using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicio_Paquetes.Domain.DTO
{
    public class ReservaOutDto
    {
        public int Id { get; set; }
        public int Pasajeros { get; set; }
        public bool Pagado { get; set; }
        public int PasajeroId { get; set; }
        public int FormaPagoId { get; set; }
        public int PaqueteId { get; set; }
        public int PrecioTotal { get; set; }
        public List<object> Excursiones { get; set; }
        public int GrupoId { get; set; }
        public int ViajeId { get; set; }
    }
}
