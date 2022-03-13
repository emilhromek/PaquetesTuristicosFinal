using System;
using System.Collections.Generic;
using System.Text;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO.Grupo;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Domain.Services
{
    public interface IGrupoService : IServicesGeneric
    {
        GrupoResponseDTO AddGrupo(GrupoDTO grupoDTO);
        GrupoResponseDTO GetGrupoById(int id);
        List<GrupoResponseDTO> GetAll();
        void DeleteGrupoById(int id);
        public List<GrupoResponseDTO> GetAllPorPaquete(int PaqueteId);
        public GrupoResponseDTO PatchGrupoViaje(int id, int viajeId);
        public GrupoResponseDTO UnpatchGrupoViaje(int id);
    }
}
