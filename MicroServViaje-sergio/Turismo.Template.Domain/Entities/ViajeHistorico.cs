using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Turismo.Template.Domain.Entities
{
    public class ViajeHistorio
    {
        public int ViajeHistoricoId { get; set; }
        public string IdentificadorUnicoDeViaje { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaVuelta { get; set; }
        public string TerminalOrigen { get; set; }
        public int InfoPasajeros { get; set; }
        public string InfoChoferesCoordinadorBus { get; set; }
        public string InfoPaquete { get; set; }
    }
}
