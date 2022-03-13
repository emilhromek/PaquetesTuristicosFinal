using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Domain.DTO
{
    public class ViajeDTO
    {
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada { get; set; }
        public int TerminalOrigen1Id { get; set; }
        public int TerminalOrigen2Id { get; set; }
        public int TerminalOrigen3Id { get; set; }
        public int GrupoId { get; set; }
        public int PaqueteId { get; set; }
        public int Chofer1Id { get; set; }
        public int Chofer2Id { get; set; }
        public int CoordinadorId { get; set; }
        public int BusId { get; set; }



    }
}
