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

namespace Tests
{
    public class BusTest
    {
        private readonly Mock<IBusRepository> _repository;
        private readonly BusService _service;
        public BusTest()
        {
            _repository = new Mock<IBusRepository>();
            _service = new BusService(_repository.Object);
        }

        [Fact]
        public void AddBusTest()
        {
            // Arrange

            var itemsInserted = new List<Bus>();

            var busDTO = new BusDTO()
            {
                Numero = "1",
                Patente = "Patente",
                Capacidad = 20,
                Observacion = "Observacion",
                EmpresaId = 1,
            };

            var busResponse = new BusResponseDTO()
            {
                Numero = "1",
            };

            var empresa = new Empresa()
            {
                EmpresaId = 1,
            };

            _repository.Setup(x => x.FindBy<Empresa>(1)).Returns(empresa);

            _repository.Setup(i => i.Add(It.IsAny<Bus>()))
            .Callback((Bus item) => itemsInserted.Add(item));

            var service = new BusService(_repository.Object);

            // Act

            var result = service.AddBus(busDTO);

            // Assert

            Assert.Single(itemsInserted);
            Assert.Equal(result.Numero, busResponse.Numero);
        }

        [Fact]
        public void GetBus()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var bus = new Bus()
            {
                BusId = 1,
                EmpresaId = 1,
            };

            var empresa = new Empresa()
            {
                EmpresaId = 1,
            };

            var response = new BusResponseDTO()
            {
                BusId = 1,
            };

            _repository.Setup(x => x.FindBy<Bus>(1)).Returns(bus);
            _repository.Setup(x => x.FindBy<Empresa>(1)).Returns(empresa);

            // Act

            var result = _service.GetBusById(1);

            // Assert

            Assert.Equal(result.BusId, response.BusId);
        }

        [Fact]
        public void GetBus_NotFound()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var bus = new Bus()
            {
                BusId = 1,
                EmpresaId = 1,
            };

            var empresa = new Empresa()
            {
                EmpresaId = 1,
            };

            var response = new BusResponseDTO()
            {
                BusId = 1,
            };

            _repository.Setup(x => x.FindBy<Bus>(1)).Returns(bus);
            _repository.Setup(x => x.FindBy<Empresa>(1)).Returns(empresa);

            // Act

            //var result = _service.GetBusById(2);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.GetBusById(2));
        }

        [Fact]
        public void GetBuses()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var busList = new List<Bus>()
            {
                new Bus()
                {
                    BusId = 1,
                    EmpresaId = 1,
                }                
            };

            var empresa = new Empresa()
            {
                EmpresaId = 1,
            };

            var response = new BusResponseDTO()
            {
                BusId = 1,
            };

            _repository.Setup(x => x.Traer<Bus>()).Returns(busList);
            _repository.Setup(x => x.FindBy<Empresa>(1)).Returns(empresa);

            // Act

            var result = _service.GetAll();

            // Assert

            Assert.Single(result);
        }

        [Fact]
        public void ActualizarBus()
        {
            // Arrange

            var bus = new Bus()
            {
                BusId = 1,
                EmpresaId = 1,
            };

            var busDto = new BusDTO()
            {
                EmpresaId = 1,
            };

            var empresa = new Empresa()
            {
                EmpresaId = 1,
            };

            _repository.Setup(x => x.FindBy<Bus>(1)).Returns(bus);
            _repository.Setup(x => x.FindBy<Empresa>(1)).Returns(empresa);

            // Act

            _service.ActualizarBus(1, busDto);

            // Assert

            _repository.Verify(d => d.Update(It.IsAny<Bus>()), Times.Once());

        }

        [Fact]
        public void ActualizarBus_NotFound()
        {
            // Arrange

            var bus = new Bus()
            {
                BusId = 1,
                EmpresaId = 1,
            };

            var busDto = new BusDTO()
            {
                EmpresaId = 1,
            };

            var empresa = new Empresa()
            {
                EmpresaId = 1,
            };

            _repository.Setup(x => x.FindBy<Bus>(1)).Returns(bus);
            _repository.Setup(x => x.FindBy<Empresa>(1)).Returns(empresa);

            // Act

            //_service.ActualizarBus(2, busDto);

            // Assert

            var ex = Assert.Throws<Exception>(()=> _service.ActualizarBus(2, busDto));


        }

        [Fact]
        public void DeleteBus()
        {
            var itemsInserted = new List<Bus>();

            itemsInserted.Add(new Bus()
            {
                BusId = 1
            });

            _repository.Setup(x => x.FindBy<Bus>(1)).Returns(itemsInserted.Find(x => x.BusId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Bus>())).Callback<Bus>((entity) => itemsInserted.Remove(entity));

            // Act

            _service.DeleteBusById(1);

            // Assert

            Assert.Empty(itemsInserted);
        }

        [Fact]
        public void DeleteBus_NotFound()
        {
            var itemsInserted = new List<Bus>();

            itemsInserted.Add(new Bus()
            {
                BusId = 1
            });

            _repository.Setup(x => x.FindBy<Bus>(1)).Returns(itemsInserted.Find(x => x.BusId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Bus>())).Callback<Bus>((entity) => itemsInserted.Remove(entity));

            // Act

            //_service.DeleteBusById(1);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.DeleteBusById(2));
        }
    }
}