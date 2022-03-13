using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.Queries;

namespace Turismo.Template.AccessData.Queries
{
    public class EmpleadoQuery : IEmpleadoQuery
    {
        private readonly IDbConnection _connection;
        private readonly Compiler _SqlKataCompiler;

        public EmpleadoQuery(IDbConnection connection, Compiler sqlKataCompiler)
        {
            _connection = connection;
            _SqlKataCompiler = sqlKataCompiler;
        }

        public List<EmpleadoByEmailDto> GetEmpleadoByEmail(string email)
        {
            var db = new QueryFactory(_connection, _SqlKataCompiler);

            var users = db.Query("Users")
                .Select()
                .Join("Empleados", "Empleados.UserId", "Users.UserId")
                .When(!string.IsNullOrWhiteSpace(email), q => q.WhereLike("Users.Email", $"%{email}%"));
            var usersresult = users.Get<UserByEmailDto>();
            List<UserDtoSinPassword> usersDto = new List<UserDtoSinPassword>();
            foreach(var u in usersresult)
            {
                var user = new UserDtoSinPassword
                {
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Email = u.Email,
                    Roll = u.RollId

                };
                usersDto.Add(user);
            }

            List<EmpleadoByEmailDto> result = new List<EmpleadoByEmailDto>();
            foreach (var c in usersresult)
            {
                var roles = db.Query("Roles")
                .Select("RollId", "Nombre", "Descripcion")
                .Where("RollId", "=", c.RollId)
                .FirstOrDefault<RollByEmailDto>();

            var empleado = db.Query("Empleados")
                .Select("Empleados.EmpleadoId","Empleados.Dni", "Empleados.Telefono", "Empleados.FechaNacimiento", "Empleados.Legajo", "Empleados.Sueldo", "Empleados.UserId")
                .Where("UserId", "=", c.UserId)
                .FirstOrDefault<EmpleadoByEmailDto>();

                var usertext = db.Query("Users")
                        .Select()
                        .Where("UserId", "=", c.UserId)
                        .FirstOrDefault<UserDtoSinPasswordSinRoll>();
                result.Add(
                      new EmpleadoByEmailDto
                      {
                          EmpleadoId = empleado.EmpleadoId,
                          Dni = empleado.Dni,
                          Telefono = empleado.Telefono,
                          FechaNacimiento = empleado.FechaNacimiento,
                          Legajo = empleado.Legajo,
                          Sueldo = empleado.Sueldo,
                          UserId = empleado.UserId,
                          User = usertext,
                          Roll = roles
                      }); ;
            }

            return result.ToList();
        }
    }
}
