using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turismo.Template.Domain.DTO.Bus
{
    public class BusDTO
    {
        public string Numero { get; set; }

        public string Patente { get; set; }

        public int Capacidad { get; set; }

        public int EmpresaId { get; set; }

        public string Observacion { get; set; }
    }
}
