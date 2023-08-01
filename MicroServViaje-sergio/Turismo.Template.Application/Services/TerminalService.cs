using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turismo.Template.Application.Services.Base;
using Turismo.Template.Domain.DTO.TerminalDTO;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;
using Turismo.Template.Domain.Services;

namespace Turismo.Template.Application.Services
{
    public class TerminalService : ServicesGeneric, ITerminalService
    {
        private readonly ITerminalRepository repository;
        public TerminalService(ITerminalRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public TerminalResponseDTO AddTerminal(TerminalDTO terminalDTO)
        {
            var terminal = new Terminal()
            {
                Nombre = terminalDTO.Nombre,
                Descripcion = terminalDTO.Descripcion
            };

            this.Repository.Add(terminal);

            return ToTerminalResponseDTO(terminal);
        }

        public TerminalResponseDTO GetTerminalById(int id)
        {
            var terminal = repository.FindBy<Terminal>(id);
            if (terminal == null)
                throw new Exception($"La terminal id:{id} no existe");
            return ToTerminalResponseDTO(terminal);
        }

        public TerminalResponseDTO ActualizarTerminal(int id, TerminalDTO terminalDTO)
        {
            var check = repository.FindBy<Terminal>(id);

            if (check == null)
                throw new Exception();

            var terminal = new Terminal()
            {
                TerminalId = id,
                Nombre = terminalDTO.Nombre,
                Descripcion = terminalDTO.Descripcion,
            };

            this.repository.Update<Terminal>(terminal);

            return ToTerminalResponseDTO(terminal);
        }

        public void DeleteTerminalById(int id)
        {
            var check = repository.FindBy<Terminal>(id);

            if (check == null)
                throw new Exception();

            repository.Delete(check);
        }

        //public List<TerminalCompletaDTO> GetCompleto()
        //{
        //    List<TerminalCompletaDTO> terminalesCompletos = new List<TerminalCompletaDTO>();
        //    var terminales = this.Repository.Traer<Terminal>();
        //    foreach (Terminal terminal in terminales)
        //    {
        //        terminalesCompletos.Add(repository.GetTerminalCompleta(terminal));
        //    }
        //    return terminalesCompletos;
        //}


        public List<TerminalResponseDTO> GetAll()
        {
            var lista_terminal = new List<TerminalResponseDTO>();
            var terminales = repository.Traer<Terminal>().ToList();
            foreach (var terminal in terminales)
            {
                lista_terminal.Add(ToTerminalResponseDTO(terminal));
            }
            return lista_terminal;
        }

        public TerminalResponseDTO ToTerminalResponseDTO(Terminal terminal)
        {
            return new TerminalResponseDTO
            {
                TerminalId = terminal.TerminalId,
                Nombre = terminal.Nombre,
                Descripcion = terminal.Descripcion
            };
        }
    }
}
