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
using Turismo.Template.Domain.DTO.Empresa;
using Turismo.Template.Domain.DTO.Chofer;

namespace Tests
{
    public class ChoferTest
    {
        private readonly Mock<IChoferRepository> _repository;
        private readonly ChoferService _service;
        public ChoferTest()
        {
            _repository = new Mock<IChoferRepository>();
            _service = new ChoferService(_repository.Object);
        }

        [Fact]
        public void AddChoferTest()
        {
            // Arrange

            var itemsInserted = new List<Chofer>();

            var choferDTO = new ChoferDTO()
            {
                Nombre = "Nombre"
            };

            var dtoResponse = new ChoferResponseDTO()
            {
                Nombre = "Nombre"
            };

            _repository.Setup(i => i.Add(It.IsAny<Chofer>()))
            .Callback((Chofer item) => itemsInserted.Add(item));

            var service = new ChoferService(_repository.Object);

            // Act

            var result = service.AddChofer(choferDTO);

            // Assert

            Assert.Single(itemsInserted);
            Assert.Equal(result.Nombre, dtoResponse.Nombre);

        }

        [Fact]
        public void ActualizarChofer()
        {
            // Arrange

            var chofer = new Chofer()
            {
                ChoferId = 1,
            };

            _repository.Setup(x => x.FindBy<Chofer>(1)).Returns(chofer);

            // Act

            _service.Update(chofer);

            // Assert

            _repository.Verify(d => d.Update(It.IsAny<Chofer>()), Times.Once());

        }

        [Fact]
        public void GetChofer()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var chofer = new Chofer()
            {
                ChoferId = 1
            };

            var response = new ChoferResponseDTO()
            {
                ChoferId = 1
            };

            _repository.Setup(x => x.FindBy<Chofer>(1)).Returns(chofer);

            // Act

            var result = _service.GetChoferById(1);

            // Assert

            Assert.Equal(result.Nombre, response.Nombre);
        }

        [Fact]
        public void GetChofer_NotFound()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var chofer = new Chofer()
            {
                ChoferId = 1
            };

            var response = new ChoferResponseDTO()
            {
                ChoferId = 1
            };

            _repository.Setup(x => x.FindBy<Chofer>(1)).Returns(chofer);

            // Act

            //var result = _service.GetChoferById(2);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.GetChoferById(2));
        }

        [Fact]
        public void GetChoferes()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var choferList = new List<Chofer>()
            {
                new Chofer()
                {
                    ChoferId = 1,
                }                
            };

            _repository.Setup(x => x.Traer<Chofer>()).Returns(choferList);

            // Act

            var result = _service.GetAll();

            // Assert

            Assert.Single(result);
        }

        [Fact]
        public void DeleteChofer()
        {
            var itemsInserted = new List<Chofer>();

            itemsInserted.Add(new Chofer()
            {
                ChoferId = 1
            });

            _repository.Setup(x => x.FindBy<Chofer>(1)).Returns(itemsInserted.Find(x => x.ChoferId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Chofer>())).Callback<Chofer>((entity) => itemsInserted.Remove(entity));

            // Act

            _service.DeleteChoferById(1);

            // Assert

            _repository.Verify(d => d.Delete(It.IsAny<Chofer>()), Times.Once());
            Assert.Empty(itemsInserted);
        }

        [Fact]
        public void DeleteChofer_NotFound()
        {
            var itemsInserted = new List<Chofer>();

            itemsInserted.Add(new Chofer()
            {
                ChoferId = 1
            });

            _repository.Setup(x => x.FindBy<Chofer>(1)).Returns(itemsInserted.Find(x => x.ChoferId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Chofer>())).Callback<Chofer>((entity) => itemsInserted.Remove(entity));

            // Act

            // _service.DeleteChoferById(2);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.DeleteChoferById(2));
        }
    }
}