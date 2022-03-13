using System;
using System.Collections.Generic;
using System.Text;

namespace Turismo.Template.Domain.Entities
{
    public class Coordinador
    {
        public int CoordinadorId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Contacto { get; set; }

        public string Email { get; set; }

        public string Agenda { get; set; }
    }
}
