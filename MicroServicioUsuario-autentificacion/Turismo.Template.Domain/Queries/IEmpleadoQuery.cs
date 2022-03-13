using System;
using System.Collections.Generic;
using System.Text;
using Turismo.Template.Domain.DTO;

namespace Turismo.Template.Domain.Queries
{
    public interface IEmpleadoQuery
    {
        List<EmpleadoByEmailDto> GetEmpleadoByEmail(string email);
    }
}
