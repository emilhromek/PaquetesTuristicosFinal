using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Turismo.Template.Domain.Entities
{
    public class Viaje
    {
        public int ViajeId { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaVuelta { get; set; }
        public int TerminalOrigen1Id { get; set; }
        public int TerminalOrigen2Id { get; set; }
        public int TerminalOrigen3Id { get; set; }
        public int BusId { get; set; }
        public int Chofer1Id { get; set; }
        public int Chofer2Id { get; set; }
        public int GrupoId { get; set; }
        public int CoordinadorId { get; set; }
        public string IdentificadorUnicoDeViaje { get; set; }
    }
}
