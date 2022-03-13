using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turismo.Template.Domain.DTO.Chofer
{
    public class AgendaChoferResponseConFormatoDTO
    {
        public int Id { get; set; }
        public int ChoferId { get; set; }
        public string FechaInicial { get; set; }
        public string FechaFinal { get; set; }
        public int ViajeId { get; set; }

    }
}
