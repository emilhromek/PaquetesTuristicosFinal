using System;
using System.Collections.Generic;
using System.Text;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.Entities;

namespace Turismo.Template.Domain.Queries
{
    public interface IUserQuery
    {
        List<UserByEmailDto> GetUserByEmail(string email);
        User GetUserLoginByEmail(string email);
    }
}
