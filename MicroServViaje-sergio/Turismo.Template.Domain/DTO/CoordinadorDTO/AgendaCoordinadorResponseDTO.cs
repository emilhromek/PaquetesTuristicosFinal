using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turismo.Template.Domain.DTO.CoordinadorDTO
{
    public class AgendaCoordinadorResponseDTO
    {
        public int Id { get; set; }
        public int CoordinadorId { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public int ViajeId { get; set; }

    }
}
