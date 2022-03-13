using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservicio_Paquetes.Domain.Entities
{
    public class ReservaHistorica
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PrecioTotal { get; set; }
        [Required]
        public int Pasajeros { get; set; }
        public bool Pagado { get; set; }
        [Required]
        public string Pasajero { get; set; }
        [Required]
        public string FormaPago { get; set; }
        [Required]
        public string Paquete { get; set; }
        public string Excursiones { get; set; }
        public string IdentificadorUnicoDeViaje { get; set; }

    }
}
