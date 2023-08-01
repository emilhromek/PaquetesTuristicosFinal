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
    public class PasajeroTest
    {
        private readonly Mock<IRepositoryGeneric> _repository;
        private readonly Mock<IPasajeroQuery> _query;
        private readonly PasajeroServices _service;
        public PasajeroTest()
        {
            _repository = new Mock<IRepositoryGeneric>();
            _query = new Mock<IPasajeroQuery>();
            _service = new PasajeroServices(_repository.Object, _query.Object);
        }

        [Fact]
        public void CreatePasajero()
        {
            // Arrange

            var itemsInserted = new List<Pasajero>();

            _repository.Setup(i => i.Add(It.IsAny<Pasajero>()))
            .Callback((Pasajero item) => itemsInserted.Add(item));

            // Act

            var dto = new PasajeroDto()
            {
                Dni = 123456789,
                UserId = 1
            };

            _service.Create(dto);

            // Assert

            Assert.Single(itemsInserted);
        }

        [Fact]
        public void ActualizarPasajero()
        {
            // Arrange

            var pasajero = new Pasajero()
            {
                PasajeroId = 1
            };

            var dto = new PasajeroDto()
            {
                Dni = 1
            };

            _repository.Setup(x => x.FindBy<Pasajero>(1)).Returns(pasajero);

            // Act

            _service.Update(1, dto);

            // Assert

            _repository.Verify(d => d.Update(It.IsAny<Pasajero>()), Times.Once());

        }

        [Fact]
        public void ActualizarPasajero_NotFound()
        {
            // Arrange

            var pasajero = new Pasajero()
            {
                PasajeroId = 1
            };

            var dto = new PasajeroDto()
            {
                Dni = 1
            };

            _repository.Setup(x => x.FindBy<Pasajero>(1)).Returns(pasajero);

            // Act

            //_service.Update(1, dto);

            // Assert

            var ex = Assert.Throws<HttpResponseException>(() => _service.Update(2, dto));

        }

        [Fact]
        public void DeletePasajero()
        {
            // Arrange

            var itemsInserted = new List<Pasajero>();

            itemsInserted.Add(new Pasajero()
            {
                PasajeroId = 1
            });

            _repository.Setup(x => x.FindBy<Pasajero>(1)).Returns(itemsInserted.Find(x => x.PasajeroId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Pasajero>())).Callback<Pasajero>((entity) => itemsInserted.Remove(entity));

            // Act

            _service.deleteId(1);

            // Assert

            Assert.Empty(itemsInserted);
        }

        [Fact]
        public void DeletePasajero_NotFound()
        {
            // Arrange

            var itemsInserted = new List<Pasajero>();

            itemsInserted.Add(new Pasajero()
            {
                PasajeroId = 1
            });

            _repository.Setup(x => x.FindBy<Pasajero>(1)).Returns(itemsInserted.Find(x => x.PasajeroId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Pasajero>())).Callback<Pasajero>((entity) => itemsInserted.Remove(entity));

            // Act

            //_service.deleteId(1);

            // Assert

            var ex = Assert.Throws<HttpResponseException>(() => _service.deleteId(2));
        }

        [Fact]
        public void GetById()
        {
            // Arrange

            var pasajero = new Pasajero()
            {
                PasajeroId = 1,
            };

            var response = new PasajeroDtoById()
            {
                PasajeroId = 1,
            };

            // Act

            _repository.Setup(x => x.FindBy<Pasajero>(1)).Returns(pasajero);

            var result = _service.getId(1);

            // Assert

            Assert.Equal(result.PasajeroId, response.PasajeroId);
        }

        [Fact]
        public void GetByEmail()
        {
            // Arrange

            var pasajeroList = new List<PasajeroByEmailDto>()
            {
                new PasajeroByEmailDto()
                {
                    UserId = 1,
                }                
            };

            var user = new User()
            {
                UserId = 1,
                Email = "a@a.com"
            };

            var response = new PasajeroDtoById()
            {
                PasajeroId = 1,
            };

            // Act

            _query.Setup(x => x.GetPasajeroByEmail("a@a.com")).Returns(pasajeroList);

            var result = _service.GetPasajeroByEmail("a@a.com");

            // Assert

            Assert.Single(result);
        }

        [Fact]
        public void GetById_NotFound()
        {
            // Arrange

            var pasajero = new Pasajero()
            {
                PasajeroId = 1,
            };

            var response = new PasajeroDtoById()
            {
                PasajeroId = 1,
            };

            _repository.Setup(x => x.FindBy<Pasajero>(1)).Returns(pasajero);

            // Act

            //var result = _service.getId(2);

            // Assert

            var ex = Assert.Throws<HttpResponseException>(() => _service.getId(2));
        }
    }
}