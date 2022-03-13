using System;
using System.Collections.Generic;
using System.Text;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO.TerminalDTO;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Domain.Services
{
    public interface ITerminalService : IServicesGeneric
    {
        TerminalResponseDTO AddTerminal(TerminalDTO terminalDTO);
        TerminalResponseDTO GetTerminalById(int id);
        List<TerminalResponseDTO> GetAll();
        public TerminalResponseDTO ActualizarTerminal(int id, TerminalDTO terminalDTO);
        void DeleteTerminalById(int id);
        //List<TerminalCompletaDTO> GetCompleto();

    }
}
