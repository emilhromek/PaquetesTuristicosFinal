using System;
using System.Collections.Generic;
using System.Text;

namespace Turismo.Template.Domain.DTO
{
    public class EmpleadoDto
    {
        public int Dni { get; set; }
        public int Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public int Legajo { get; set; }
        public double Sueldo { get; set; }

        public int UserId { get; set; }
    }
    public class EmpleadoDtoById
    {
        public int EmpleadoId {get; set;}
        public int Dni { get; set; }
        public int Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public int Legajo { get; set; }
        public double Sueldo { get; set; }

        public int UserId { get; set; }
    }
}
