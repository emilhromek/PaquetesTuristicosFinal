using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turismo.Template.Domain.Entities
{
    public class Chofer
    {
        public int ChoferId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Contacto { get; set; }

        public DateTime Vencimiento { get; set; }

        public string Licencia { get; set; }

        public string Agenda { get; set; }

    }
}
