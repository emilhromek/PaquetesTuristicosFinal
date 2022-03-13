using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservicio_Paquetes.Domain.Entities
{
    public class Reserva
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PrecioTotal { get; set; }
        [Required]
        public int Pasajeros { get; set; }
        public bool Pagado { get; set; }
        [Required]
        public int PasajeroId { get; set; }
        [Required]
        public int FormaPagoId { get; set; }
        public FormaPago FormaPago { get; set; }
        [Required]
        public int PaqueteId { get; set; }
        public int GrupoId { get; set; }
        public int ViajeId { get; set; }
        public Paquete Paquete { get; set; }
        public ICollection<ReservaExcursion> ReservaExcursiones { get; set; }

    }
}
