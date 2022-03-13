using System;
using System.Collections.Generic;
using System.Text;

namespace Turismo.Template.Domain.DTO
{
    public class PasajeroDto
    {
        public int UserId { get; set; }
        public int Dni { get; set; }
        public int Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
    public class PasajeroDtoById
    {
        public int PasajeroId { get; set; }
        public int Dni { get; set; }
        public int Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public int UserId { get; set; }
    }
}
