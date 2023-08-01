using Moq;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Turismo.Template.Application.Services;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;
using Xunit;

namespace UnitTest
{
    public class UserTest
    {
        private readonly Mock<IRepositoryGeneric> _repository;
        private readonly Mock<IUserQuery> _query;
        private readonly UserService _service;
        public UserTest()
        {
            _repository = new Mock<IRepositoryGeneric>();
            _query = new Mock<IUserQuery>();
            _service = new UserService(_repository.Object, _query.Object);
        }

        [Fact]
        public void CreateUser()
        {
            // Arrange

            var itemsInserted = new List<User>();

            _repository.Setup(i => i.Add(It.IsAny<User>()))
            .Callback((User item) => itemsInserted.Add(item));

            // Act

            var dto = new UserDto()
            {
                Nombre = "Nombre",
            };

            _service.CreateUser(dto);

            // Assert

            Assert.Single(itemsInserted);
        }

        [Fact]
        public void ActualizarUser()
        {
            // Arrange

            var user = new User()
            {
                UserId = 1
            };

            var dto = new UserDto()
            {
                Nombre = "Nombre"
            };

            _repository.Setup(x => x.FindBy<User>(1)).Returns(user);

            // Act

            _service.Update(1, dto);

            // Assert

            _repository.Verify(d => d.Update(It.IsAny<User>()), Times.Once());

        }

        [Fact]
        public void ActualizarUser_NotFound()
        {
            // Arrange

            var user = new User()
            {
                UserId = 1
            };

            var dto = new UserDto()
            {
                Nombre = "Nombre"
            };

            _repository.Setup(x => x.FindBy<User>(1)).Returns(user);

            // Act

            //_service.Update(2, dto);

            // Assert

            var ex = Assert.Throws<HttpResponseException>(() => _service.Update(2, dto));

        }

        [Fact]
        public void DeleteUser()
        {
            // Arrange

            var itemsInserted = new List<User>();

            itemsInserted.Add(new User()
            {
                UserId = 1
            });

            _repository.Setup(x => x.FindBy<User>(1)).Returns(itemsInserted.Find(x => x.UserId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<User>())).Callback<User>((entity) => itemsInserted.Remove(entity));

            // Act

            _service.deleteUserId(1);

            // Assert

            Assert.Empty(itemsInserted);
        }

        [Fact]
        public void DeleteUser_NotFound()
        {
            // Arrange

            var itemsInserted = new List<User>();

            itemsInserted.Add(new User()
            {
                UserId = 1
            });

            _repository.Setup(x => x.FindBy<User>(1)).Returns(itemsInserted.Find(x => x.UserId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<User>())).Callback<User>((entity) => itemsInserted.Remove(entity));

            // Act

            //_service.deleteUserId(2);

            // Assert

            var ex = Assert.Throws<HttpResponseException>(() => _service.deleteUserId(2));
        }

        [Fact]
        public void GetById()
        {
            // Arrange

            var user = new User()
            {
                UserId = 1,
            };

            var response = new UserByIdDto()
            {
                UserId = 1,
            };

            // Act

            _repository.Setup(x => x.FindBy<User>(1)).Returns(user);

            var result = _service.getUserId(1);

            // Assert

            Assert.Equal(result.UserId, response.UserId);
        }

        [Fact]
        public void GetById_NotFound()
        {
            // Arrange

            var user = new User()
            {
                UserId = 1,
            };

            var response = new UserByIdDto()
            {
                UserId = 1,
            };

            _repository.Setup(x => x.FindBy<User>(1)).Returns(user);

            // Act

            //var result = _service.getUserId(2);

            // Assert

            var ex = Assert.Throws<HttpResponseException>(() => _service.getUserId(2));
        }

        [Fact]
        public void GetByEmail()
        {
            // Arrange

            var user = new User()
            {
                UserId = 1,
                Email = "a@a.com"
            };

            // Act

            _query.Setup(x => x.GetUserByEmail("a@a.com")).Returns(new List<UserByEmailDto>
            {
                new UserByEmailDto()
                {
                    UserId = user.UserId,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Email = user.Email,
                    RollId = user.RollId,
                }
            });

            var result = _service.GetUserByEmail("a@a.com");

            // Assert

            Assert.Single(result);
        }

        [Fact]
        public void Authenticate()
        {
            // Arrange

            var user = new User()
            {
                Email = "a@a.com",
                Password = "1234"
            };

            _query.Setup(x => x.GetUserLoginByEmail("a@a.com")).Returns(user);

            // Act

            var result = _service.Authenticate("a@a.com", "1234");

            // Assert

            Assert.Equal(user, result);
        }

        [Fact]
        public void Authenticate_UsuarioNoExiste()
        {
            // Arrange

            var user = new User()
            {
                Email = "a@a.com",
                Password = "1234"
            };

            _query.Setup(x => x.GetUserLoginByEmail("a@a.com")).Returns(user);

            // Act

            var result = _service.Authenticate("b@a.com", "1234");

            // Assert

            Assert.Null(result);
        }

        [Fact]
        public void Authenticate_PasswordIncorrecto()
        {
            // Arrange

            var user = new User()
            {
                Email = "a@a.com",
                Password = "1234"
            };

            _query.Setup(x => x.GetUserLoginByEmail("a@a.com")).Returns(user);

            // Act

            var result = _service.Authenticate("a@a.com", "1235");

            // Assert

            Assert.Null(result);
        }
    }
}