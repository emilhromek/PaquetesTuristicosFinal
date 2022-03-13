using System;
using System.Collections.Generic;
using System.Text;
using Turismo.Template.AccessData.Command;
using Turismo.Template.Domain.DTO.CoordinadorDTO;
using Turismo.Template.Domain.DTO.TerminalDTO;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;

namespace Turismo.Template.AccessData.Queries
{
    public class TerminalRepository : GenericsRepository, ITerminalRepository
    {
        private readonly DbContextGeneric context;

        public TerminalRepository(DbContextGeneric contexto) : base(contexto)
        {
            this.context = contexto;
        }


        
        public TerminalDTO GetTerminalById(Terminal terminaloriginal)
        {
            throw new NotImplementedException();
        }
        //public TerminalCompletaDTO GetTerminalCompleta(Terminal t)
        //{
        //    throw new NotImplementedException();
        //}

    }

}
