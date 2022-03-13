using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turismo.Template.Domain.DTO.Bus
{
    public class AgendaBusResponseConFormatoDTO
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public string FechaInicial { get; set; }
        public string FechaFinal { get; set; }
        public int ViajeId { get; set; }

    }
}
