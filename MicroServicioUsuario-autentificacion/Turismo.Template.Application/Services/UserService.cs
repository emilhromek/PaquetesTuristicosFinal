using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Http;
using Turismo.Template.Application.Helpers;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;

namespace Turismo.Template.Application.Services
{

    public interface IUserService
    {
        User CreateUser(UserDto user);
        IEnumerable<User> getUser();
        UserByIdDto getUserId(int id);
        void deleteUserId(int id);
        List<UserByEmailDto> GetUserByEmail(string email);
        UserDto Update(int id, UserDto user);
        User Authenticate(string username, string password);
    }
    public class UserService : IUserService
    {
        private readonly IRepositoryGeneric _repository;
        private readonly IUserQuery _query;
        public UserService(IRepositoryGeneric repository, IUserQuery query)
        {
            _repository = repository;
            _query = query;
        }

        public User CreateUser(UserDto user)
        {
            var entity = new User
            {
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Email = user.Email,
                Password = user.Password,
                RollId = user.Roll
            };
            _repository.Add<User>(entity);
            return entity;
        }
        public IEnumerable<User> getUser()
        {
            return _repository.Traer<User>();
        }

        public UserByIdDto getUserId(int id)
        {
            var user= _repository.FindBy<User>(id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return new UserByIdDto
            {
                UserId = user.UserId,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Email = user.Email,
                RollId = user.RollId

            };
        }

        public void deleteUserId(int id)
        {
            var user = _repository.FindBy<User>(id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                _repository.DeleteBy<User>(id);
            }
            
        }

        public List<UserByEmailDto> GetUserByEmail(string email)
        {
            return _query.GetUserByEmail(email);
        }

        public UserDto Update(int id, UserDto user)
        {
            var entity = new User
            {
                UserId = id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Email = user.Email,
                Password = user.Password,
                RollId = user.Roll
            };
            _repository.Update<User>(entity);
            return new UserDto
            {
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Email = user.Email,
                Password = "*****",
                Roll = user.Roll
            };
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _query.GetUserLoginByEmail(username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (password != user.Password)
                return null;

            // authentication successful
            return user;
        }

    }

}
