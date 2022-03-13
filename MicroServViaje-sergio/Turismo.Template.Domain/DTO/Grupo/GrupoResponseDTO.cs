using System;
using System.Collections.Generic;
using System.Text;

namespace Turismo.Template.Domain.DTO.Grupo
{
    public class GrupoResponseDTO
    {
        public int GrupoId { get; set; }
        public int PaqueteId { get; set; }

        public int TotalPasajeros { get; set; }
        public int ViajeId { get; set; }
    }
}
