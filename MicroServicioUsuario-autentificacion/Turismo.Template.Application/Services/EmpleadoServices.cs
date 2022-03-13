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
    public interface IEmpleadoServices
    {
        Empleado Create(EmpleadoDto empleado);
        IEnumerable<Empleado> getAll();
        EmpleadoDtoById getId(int id);
        void deleteId(int id);
        List<EmpleadoByEmailDto> GetEmpleadoByEmail(string email);
        EmpleadoDto Update(int id, EmpleadoDto empleado);
    }
    public class EmpleadoServices : IEmpleadoServices
    {
        private readonly IRepositoryGeneric _repository;
        private readonly IEmpleadoQuery _query;
        public EmpleadoServices(IRepositoryGeneric repository, IEmpleadoQuery query)
        {
            _repository = repository;
            _query = query;
        }
        public Empleado Create(EmpleadoDto empleado)
        {
            var entity = new Empleado
            {
                Dni = empleado.Dni,
                Telefono = empleado.Telefono,
                FechaNacimiento = empleado.FechaNacimiento,
                Sueldo = empleado.Sueldo,
                Legajo = empleado.Legajo,
                UserId = empleado.UserId,

            };
            _repository.Add<Empleado>(entity);
            return entity;
        }

        public void deleteId(int id)
        {
            _repository.DeleteBy<Empleado>(id);
        }

        public IEnumerable<Empleado> getAll()
        {
            return _repository.Traer<Empleado>();
        }

        public EmpleadoDtoById getId(int id)
        {
            var empleado=  _repository.FindBy<Empleado>(id);
            if (empleado == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return new EmpleadoDtoById
            {
                EmpleadoId =empleado.EmpleadoId,
                Dni = empleado.Dni,
                Telefono = empleado.Telefono,
                FechaNacimiento = empleado.FechaNacimiento,
                Legajo = empleado.Legajo,
                Sueldo = empleado.Sueldo,
                UserId = empleado.UserId
            };
        }

        public List<EmpleadoByEmailDto> GetEmpleadoByEmail(string email)
        {
            return _query.GetEmpleadoByEmail(email);
        }
        public EmpleadoDto Update(int id, EmpleadoDto empleado)
        {
            var entity = new Empleado
            {
                EmpleadoId = id,
                Dni = empleado.Dni,
                Telefono = empleado.Telefono,
                FechaNacimiento = empleado.FechaNacimiento,
                Legajo = empleado.Legajo,
                Sueldo = empleado.Sueldo,
                UserId = empleado.UserId
            };
            _repository.Update<Empleado>(entity);
            return new EmpleadoDto
            {
                Dni = empleado.Dni,
                Telefono = empleado.Telefono,
                FechaNacimiento = empleado.FechaNacimiento,
                Legajo = empleado.Legajo,
                Sueldo = empleado.Sueldo,
                UserId = empleado.UserId
            };
        }
    }
}
