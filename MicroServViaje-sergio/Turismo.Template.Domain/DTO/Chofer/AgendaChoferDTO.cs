using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turismo.Template.Domain.DTO.Chofer
{
    public class AgendaChoferDTO
    {
        public int ChoferId { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public int ViajeId { get; set; }

    }
}
