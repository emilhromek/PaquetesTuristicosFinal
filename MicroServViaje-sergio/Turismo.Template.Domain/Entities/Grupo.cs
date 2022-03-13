using System;
using System.Collections.Generic;
using System.Text;

namespace Turismo.Template.Domain.Entities
{
    public class Grupo
    {
        public int GrupoId { get; set; }

        public int TotalPasajeros { get; set; }

        public int PaqueteId { get; set; }
        public int ViajeId { get; set; }
        public int BusId { get; set; }
        public int CapacidadDeBus { get; set; }

        //public int CoordinadorId { get; set; }
        //public virtual Coordinador Coordinador { get; set; }
    }
}
