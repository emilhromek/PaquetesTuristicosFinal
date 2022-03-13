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
    public class PasajeroQuery : IPasajeroQuery
    {
        private readonly IDbConnection _connection;
        private readonly Compiler _SqlKataCompiler;

        public PasajeroQuery(IDbConnection connection, Compiler sqlKataCompiler)
        {
            _connection = connection;
            _SqlKataCompiler = sqlKataCompiler;
        }

        public List<PasajeroByEmailDto> GetPasajeroByEmail(string email)
        {
            var db = new QueryFactory(_connection, _SqlKataCompiler);

            var users = db.Query("Users")
                .Select()
                .Join("Pasajeros", "Pasajeros.UserId", "Users.UserId")
                .When(!string.IsNullOrWhiteSpace(email), q => q.WhereLike("Users.Email", $"%{email}%"));
            var usersresult = users.Get<UserByEmailDto>();
            List<UserDtoSinPassword> usersDto = new List<UserDtoSinPassword>();
            foreach (var u in usersresult)
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

            List<PasajeroByEmailDto> result = new List<PasajeroByEmailDto>();
            foreach (var c in usersresult)
            {
                var roles = db.Query("Roles")
                .Select("RollId", "Nombre", "Descripcion")
                .Where("RollId", "=", c.RollId)
                .FirstOrDefault<RollByEmailDto>();

                var pasajero = db.Query("Pasajeros")
                    .Select("Pasajeros.PasajeroId","Pasajeros.Dni", "Pasajeros.Telefono", "Pasajeros.FechaNacimiento", "Pasajeros.UserId")
                    .Where("UserId", "=", c.UserId)
                    .FirstOrDefault<PasajeroByEmailDto>();
                var usertext = db.Query("Users")
                    .Select()
                    .Where("UserId", "=", c.UserId)
                    .FirstOrDefault<UserDtoSinPasswordSinRoll>();
                result.Add(
                          new PasajeroByEmailDto
                          {
                              pasajeroId = pasajero.pasajeroId,
                              Dni = pasajero.Dni,
                              Telefono = pasajero.Telefono,
                              FechaNacimiento = pasajero.FechaNacimiento,
                              UserId = pasajero.UserId,
                              User = usertext,
                              Roll = roles
                          });
            }

            return result.ToList();
        }
    }
}
