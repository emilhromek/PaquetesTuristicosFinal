using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO.Bus;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Domain.Queries
{
    public interface IBusRepository : IRepositoryGeneric 
    {
        //public ViajeConBusTodoDTO GetBusByIdConEmpresa(Bus busOriginal);
    }
}
