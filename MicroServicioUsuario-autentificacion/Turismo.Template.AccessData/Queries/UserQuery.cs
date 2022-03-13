using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;

namespace Turismo.Template.AccessData.Queries
{
    public class UserQuery : IUserQuery
    {
        private readonly IDbConnection _connection;
        private readonly Compiler _SqlKataCompiler;

        public UserQuery(IDbConnection connection, Compiler sqlKataCompiler)
        {
            _connection = connection;
            _SqlKataCompiler = sqlKataCompiler;
        }

        public List<UserByEmailDto> GetUserByEmail(string email)
        {
            var db = new QueryFactory(_connection, _SqlKataCompiler);

            var users = db.Query("Users")
                .Select("Users.UserId",
                "Users.Nombre",
                "Users.Apellido",
                "Users.Email",
                "Users.RollId")
                .Join("Roles","Roles.RollId","Users.RollId")
                .When(!string.IsNullOrWhiteSpace(email), q => q.WhereLike("Users.Email", $"%{email}%"));
            var usersresult = users.Get<UserByEmailDto>();
            List<UserByEmailDto> result = new List<UserByEmailDto>();
            foreach (var c in usersresult)
            {
                var roles = db.Query("Roles")
                    .Select("RollId", "Nombre", "Descripcion")
                    .Where("RollId", "=", c.RollId)
                    .FirstOrDefault<RollByEmailDto>();

                result.Add(
                      new UserByEmailDto
                       {
                          UserId = c.UserId,
                          Nombre = c.Nombre,
                          Apellido = c.Apellido,
                          Email = c.Email,
                          RollId = c.RollId,
                          Roll = roles
                       });
            }
            return result.ToList();
        }

        public User GetUserLoginByEmail(string email)
        {
            var db = new QueryFactory(_connection, _SqlKataCompiler);
            var user = db.Query("Users").Select("UserId","Nombre","Apellido","Email","Password","RollId").Where("Email", "=", email).FirstOrDefault<User>();
            if(user != null)
            {
                return new User
                {
                    UserId = user.UserId,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Email = user.Email,
                    Password = user.Password,
                    RollId = user.RollId
                };
            }
            else
            {
                return new User();
            }
        }
    }
}
