using System;
using System.Collections.Generic;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO.Empresa;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Domain.Services
{
    public interface IEmpresaService : IServicesGeneric 
    {
        EmpresaResponseDTO AddEmpresa(EmpresaDTO empresaDTO);
        EmpresaResponseDTO GetEmpresaById(int id);
        List<EmpresaResponseDTO> GetAll();
        public EmpresaResponseDTO ActualizarEmpresa(int id, EmpresaDTO empresaDTO);

        void DeleteEmpresaById(int id);
        //List<EmpresaCompletaDTO> GetCompleto();

    }
}
