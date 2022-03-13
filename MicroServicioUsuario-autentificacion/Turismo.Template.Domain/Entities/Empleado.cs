using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Turismo.Template.Domain.Entities
{
    public class Empleado
    {
        [Required]
        public int EmpleadoId { get; set; }
        [Required]
        public int Dni { get; set; }
        [Required]
        public int Telefono { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }

        public int Legajo { get; set; }
        public double Sueldo { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }
    }
}
