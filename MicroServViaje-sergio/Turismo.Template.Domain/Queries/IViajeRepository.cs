using System;
using System.Collections.Generic;
using System.Text;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.DTO.ViajeResponseDTO;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Domain.Queries
{
    public interface IViajeRepository : IRepositoryGeneric
    {
        //public ClaseConCuestionarioDTO GetClaseByIdConCuestionarios(Viaje viajeOriginal);
        public ViajeDTO GetViajeById(Viaje viajeoriginal);
        //public ViajeCompletoDTO GetViajeCompleto(Viaje c);
    }
}
