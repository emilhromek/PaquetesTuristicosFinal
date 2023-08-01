using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turismo.Template.Application.Services.Base;
using Turismo.Template.Domain.DTO.Bus;
using Turismo.Template.Domain.Queries;
using Turismo.Template.Domain.Services;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Application.Services;
using Turismo.Template.Domain.DTO.CoordinadorDTO;

namespace Tests
{
    public class CoordinadorTest
    {
        private readonly Mock<ICoordinadorRepository> _repository;
        private readonly CoordinadorService _service;
        public CoordinadorTest()
        {
            _repository = new Mock<ICoordinadorRepository>();
            _service = new CoordinadorService(_repository.Object);
        }

        [Fact]
        public void AddCoordinadorTest()
        {
            // Arrange

            var itemsInserted = new List<Coordinador>();

            var coordinadorDTO = new CoordinadorDTO()
            {
                Nombre = "Nombre"
            };

            var dtoResponse = new CoordinadorResponseDTO()
            {
                Nombre = "Nombre"
            };

            _repository.Setup(i => i.Add(It.IsAny<Coordinador>()))
            .Callback((Coordinador item) => itemsInserted.Add(item));

            var service = new CoordinadorService(_repository.Object);

            // Act

            var result = service.AddCoordinador(coordinadorDTO);

            // Assert

            Assert.Single(itemsInserted);
            Assert.Equal(result.Nombre, dtoResponse.Nombre);

        }

        [Fact]
        public void ActualizarCoordinador()
        {
            // Arrange

            var coordinador = new Coordinador()
            {
                CoordinadorId = 1,
            };

            _repository.Setup(x => x.FindBy<Coordinador>(1)).Returns(coordinador);

            // Act

            _service.Update(coordinador);

            // Assert

            _repository.Verify(d => d.Update(It.IsAny<Coordinador>()), Times.Once());

        }

        [Fact]
        public void GetCoordinador()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var coordinador = new Coordinador()
            {
                Nombre = "Nombre"
            };

            var response = new CoordinadorResponseDTO()
            {
                Nombre = "Nombre"
            };

            _repository.Setup(x => x.FindBy<Coordinador>(1)).Returns(coordinador);

            // Act

            var result = _service.GetCoordinadorById(1);

            // Assert

            Assert.Equal(result.Nombre, response.Nombre);
        }

        [Fact]
        public void GetCoordinador_NotFound()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var coordinador = new Coordinador()
            {
                Nombre = "Nombre"
            };

            var response = new CoordinadorResponseDTO()
            {
                Nombre = "Nombre"
            };

            _repository.Setup(x => x.FindBy<Coordinador>(1)).Returns(coordinador);

            // Act

            //var result = _service.GetCoordinadorById(2);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.GetCoordinadorById(2));
        }

        [Fact]
        public void GetCoordinadores()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var coordinadorList = new List<Coordinador>()
            {
                new Coordinador()
                {
                    CoordinadorId = 1,
                }                
            };

            var response = new CoordinadorResponseDTO()
            {
                CoordinadorId = 1,
            };

            _repository.Setup(x => x.Traer<Coordinador>()).Returns(coordinadorList);

            // Act

            var result = _service.GetAll();

            // Assert

            Assert.Single(result);
        }

        [Fact]
        public void DeleteCoordinador()
        {
            var itemsInserted = new List<Coordinador>();

            itemsInserted.Add(new Coordinador()
            {
                CoordinadorId = 1
            });

            _repository.Setup(x => x.FindBy<Coordinador>(1)).Returns(itemsInserted.Find(x => x.CoordinadorId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Coordinador>())).Callback<Coordinador>((entity) => itemsInserted.Remove(entity));

            // Act

            _service.DeleteCoordinadorById(1);

            // Assert

            _repository.Verify(d => d.Delete(It.IsAny<Chofer>()), Times.Never());
            Assert.Empty(itemsInserted);
        }

        [Fact]
        public void DeleteCoordinador_NotFound()
        {
            var itemsInserted = new List<Coordinador>();

            itemsInserted.Add(new Coordinador()
            {
                CoordinadorId = 1
            });

            _repository.Setup(x => x.FindBy<Coordinador>(1)).Returns(itemsInserted.Find(x => x.CoordinadorId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Coordinador>())).Callback<Coordinador>((entity) => itemsInserted.Remove(entity));

            // Act

            //_service.DeleteCoordinadorById(2);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.DeleteCoordinadorById(2));
        }

    }
}