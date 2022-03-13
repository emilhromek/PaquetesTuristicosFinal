using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.DTO.ViajeResponseDTO;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Domain.Services
{
    public interface IViajeServices : IServicesGeneric
    {
        ViajeResponseDTO AddDesignacion(int viajeId, DesignacionDTO designacionDTO);
        ViajeResponseDTO AddViaje(ViajeDTO viajeDTO);

        //Task<ViajeCompletoDTO> GetViajesById(int idViaje);
        //List<ViajeCompletoDTO> GetCompleto();

        ViajeResponseDTO GetViajeById(int id);
        List<ViajeResponseDTO> GetAll();

        List<ViajeResponseDTO> GetBy(int paqueteId);
    }
}
