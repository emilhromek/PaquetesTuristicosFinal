using System;
using System.Collections.Generic;
using System.Text;

namespace Turismo.Template.Domain.DTO.ViajeResponseDTO
{
    public class ViajeResponseDTO
    {
        public int ViajeId { get; set; }
        public int GrupoId { get; set; }
        public string FechaSalida { get; set; }
        public string FechaSalidaSinFormato { get; set; }
        public string FechaLlegada { get; set; }
        public string FechaLlegadaSinFormato { get; set; }
        public string HoraSalida { get; set; }
        public string HoraVuelta { get; set; }
        public int TerminalOrigen1Id { get; set; }

        public string TerminalOrigen1 { get; set; }
        public int TerminalOrigen2Id { get; set; }

        public string TerminalOrigen2 { get; set; }
        public int TerminalOrigen3Id { get; set; }

        public string TerminalOrigen3 { get; set; }

        public int BusId { get; set; }

        public string BusDescripcion { get; set; }
        public int CoordinadorId { get; set; }
        public string Coordinador { get; set; }

        public int Chofer1Id { get; set; }

        public string Chofer1 { get; set; }

        public string Contacto1 { get; set; }

        public int Chofer2Id { get; set; }

        public string Chofer2 { get; set; }

        public string Contacto2 { get; set; }
        
    }
}
