using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Http;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;

namespace Turismo.Template.Application.Services
{
    public interface IRollServices
    {
        IEnumerable<RollDto> getAll();
        RollDto getId(int id);
    }
    public class RollServices : IRollServices
    {
        private readonly IRepositoryGeneric _repository;
        private readonly IUserQuery _query;
        public RollServices(IRepositoryGeneric repository, IUserQuery query)
        {
            _repository = repository;
            _query = query;
        }

        public IEnumerable<RollDto> getAll()
        {
            var user = _repository.Traer<Roll>();
            List<RollDto> roles = new List<RollDto>();
            foreach(var r in user)
            {
                var roll = new RollDto
                {
                    RollId = r.RollId,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion
                };
                roles.Add(roll); 
            }
            return roles;
        }

        public RollDto getId(int id)
        {
            var user = _repository.FindBy<Roll>(id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return new RollDto
            {
                RollId = user.RollId,
                Nombre = user.Nombre,
                Descripcion = user.Descripcion
            };
        }
    }
}
