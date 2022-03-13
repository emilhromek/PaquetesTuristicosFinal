using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turismo.Template.AccessData.Command;
using Turismo.Template.Domain.Queries;

namespace Turismo.Template.AccessData.Queries
{
    public class BusRepository : GenericsRepository, IBusRepository
    {
        private readonly DbContextGeneric context;

        public BusRepository(DbContextGeneric contexto) : base(contexto)
        {
            this.context = contexto;
        }

    }
}
