using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turismo.Template.Domain.DTO.Bus
{
    public class BusResponseDTO
    {
        public int BusId { get; set; }

        public string Numero { get; set; }

        public string Patente { get; set; }

        public int Capacidad { get; set; }

        public string Observacion { get; set; }

        public int EmpresaId { get; set; }

        public string Empresa { get; set; }

        public string EmpresaContacto { get; set; }

        public string EmpresaEmail { get; set; }
    }
}
