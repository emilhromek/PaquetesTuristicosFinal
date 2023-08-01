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
    public interface IPasajeroServices
    {
        Pasajero Create(PasajeroDto pasajero);
        IEnumerable<Pasajero> getAll();
        PasajeroDtoById getId(int id);
        void deleteId(int id);
        List<PasajeroByEmailDto> GetPasajeroByEmail(string email);
        PasajeroDto Update(int id, PasajeroDto pasajero);
    }
    public class PasajeroServices : IPasajeroServices
    {
        private readonly IRepositoryGeneric _repository;
        private readonly IPasajeroQuery _query;
        public PasajeroServices(IRepositoryGeneric repository, IPasajeroQuery query)
        {
            _repository = repository;
            _query = query;
        }
        public Pasajero Create(PasajeroDto pasajero)
        {
            var entity = new Pasajero
            {
                Dni = pasajero.Dni,
                Telefono = pasajero.Telefono,
                FechaNacimiento = pasajero.FechaNacimiento,
                UserId = pasajero.UserId,

            };
            _repository.Add<Pasajero>(entity);
            return entity;
        }

        public void deleteId(int id)
        {
            var p = _repository.FindBy<Pasajero>(id);
            if (p == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                _repository.Delete<Pasajero>(p);
            }
        }

        public IEnumerable<Pasajero> getAll()
        {
            return _repository.Traer<Pasajero>();
        }

        public PasajeroDtoById getId(int id)
        {
            var pasajero = _repository.FindBy<Pasajero>(id);
            if (pasajero == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return new PasajeroDtoById
            {
                PasajeroId = pasajero.PasajeroId,
                Dni = pasajero.Dni,
                Telefono = pasajero.Telefono,
                FechaNacimiento = pasajero.FechaNacimiento,

                UserId = pasajero.UserId
            };
        }

        public List<PasajeroByEmailDto> GetPasajeroByEmail(string email)
        {
            return _query.GetPasajeroByEmail(email);
        }
        public PasajeroDto Update(int id, PasajeroDto pasajeroDto)
        {
            var pasajero = _repository.FindBy<Pasajero>(id);
            if (pasajero == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var entity = new Pasajero
            {
                PasajeroId = id,
                Dni = pasajeroDto.Dni,
                Telefono = pasajeroDto.Telefono,
                FechaNacimiento = pasajeroDto.FechaNacimiento,
                UserId = pasajeroDto.UserId
            };
            _repository.Update<Pasajero>(entity);
            return new PasajeroDto
            {
                Dni = pasajeroDto.Dni,
                Telefono = pasajeroDto.Telefono,
                FechaNacimiento = pasajeroDto.FechaNacimiento,
                UserId = pasajeroDto.UserId
            };
        }
    }
}
