using System;
using System.Collections.Generic;
using System.Text;
using Turismo.Template.AccessData.Command;
using Turismo.Template.Domain.DTO.CoordinadorDTO;
using Turismo.Template.Domain.DTO.Grupo;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;

namespace Turismo.Template.AccessData.Queries
{
    public class GrupoRepository : GenericsRepository, IGrupoRepository
    {
        private readonly DbContextGeneric context;

        public GrupoRepository(DbContextGeneric contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public bool EliminarGrupo(int grupoId)
        {
            throw new NotImplementedException();
        }

        public GrupoDTO GetGrupoById(Grupo grupooriginal)
        {
            throw new NotImplementedException();
        }

        
    }
}
