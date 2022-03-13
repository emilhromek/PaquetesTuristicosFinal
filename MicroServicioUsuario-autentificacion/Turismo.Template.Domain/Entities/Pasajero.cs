using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Turismo.Template.Domain.Entities
{
    public class Pasajero
    {
        [Required]
        public int PasajeroId { get; set; }
        [Required]
        public int Dni { get; set; }
        [Required]
        public int Telefono { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }


        public int UserId { get; set; }
        public User user { get; set; }
        public MetodoPago MetodosPagos { get; set; }

    }
}
