using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turismo.Template.AccessData.Command;
using Turismo.Template.Domain.DTO.Chofer;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;

namespace Turismo.Template.AccessData.Queries
{
    public class ChoferRepository : GenericsRepository, IChoferRepository
    {
        private readonly DbContextGeneric context;

        public ChoferRepository(DbContextGeneric contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public ChoferDTO GetChoferById(Chofer grupooriginal)
        {
            throw new NotImplementedException();
        }
    }
}
