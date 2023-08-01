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
using Turismo.Template.Domain.DTO.TerminalDTO;
using Turismo.Template.Domain.DTO.ViajeResponseDTO;
using Turismo.Template.Domain.DTO;

namespace Tests
{
    public class ViajeTest
    {
        private readonly Mock<IViajeRepository> _repository;
        private readonly ViajeServices _service;
        public ViajeTest()
        {
            _repository = new Mock<IViajeRepository>();
            _service = new ViajeServices(_repository.Object);
        }

        [Fact]
        public void AddViajeTest()
        {
            // Arrange

            var terminal1 = new Terminal()
            {
                TerminalId = 1,
                Nombre = "Nombre1"
            };
            var terminal2 = new Terminal()
            {
                TerminalId = 2,
                Nombre = "Nombre2"
            };
            var terminal3 = new Terminal()
            {
                TerminalId = 3,
                Nombre = "Nombre3"
            };

            var itemsInserted = new List<Viaje>();

            var viajeDTO = new ViajeDTO()
            {
                TerminalOrigen1Id = 1,
                TerminalOrigen2Id = 2,
                TerminalOrigen3Id = 3,
                Chofer1Id = 0,
                Chofer2Id = 0,
                BusId = 0
            };

            var dtoResponse = new ViajeResponseDTO()
            {
                TerminalOrigen1Id = 1,
                TerminalOrigen2Id = 2,
                TerminalOrigen3Id = 3,
            };

            _repository.Setup(x => x.FindBy<Terminal>(1)).Returns(terminal1);
            _repository.Setup(x => x.FindBy<Terminal>(2)).Returns(terminal2);
            _repository.Setup(x => x.FindBy<Terminal>(3)).Returns(terminal3);
            _repository.Setup(i => i.Add(It.IsAny<Viaje>()))
            .Callback((Viaje item) => itemsInserted.Add(item));

            // Act

            var result = _service.AddViaje(viajeDTO);

            // Assert

            Assert.Single(itemsInserted);
            Assert.Equal(result.TerminalOrigen1Id, dtoResponse.TerminalOrigen1Id);

        }
        //}

        [Fact]
        public void ActualizarViaje()
        {
            // Arrange

            var viaje = new Viaje()
            {
                ViajeId = 1
            };

            var bus = new Bus()
            {
                BusId = 1,
                Capacidad = 90
            };

            var grupo = new Grupo()
            {
                GrupoId = 1,
                TotalPasajeros = 50,
            };

            var empresa = new Empresa()
            {
                EmpresaId = 1,
            };

            var terminal1 = new Terminal()
            {
                TerminalId = 1,
                Nombre = "Nombre1"
            };
            var terminal2 = new Terminal()
            {
                TerminalId = 2,
                Nombre = "Nombre2"
            };
            var terminal3 = new Terminal()
            {
                TerminalId = 3,
                Nombre = "Nombre3"
            };


            _repository.Setup(x => x.FindBy<Viaje>(1)).Returns(viaje);
            _repository.Setup(x => x.FindBy<Bus>(1)).Returns(bus);
            _repository.Setup(x => x.FindBy<Grupo>(1)).Returns(grupo);
            _repository.Setup(x => x.FindBy<Empresa>(1)).Returns(empresa);
            _repository.Setup(x => x.FindBy<Terminal>(1)).Returns(terminal1);
            _repository.Setup(x => x.FindBy<Terminal>(2)).Returns(terminal2);
            _repository.Setup(x => x.FindBy<Terminal>(3)).Returns(terminal3);

            var viajeDto = new ViajeDTO()
            {
                TerminalOrigen1Id= 1,
                TerminalOrigen2Id= 2,
                TerminalOrigen3Id= 3,
                GrupoId = 1,
                BusId = 1
            };

            // Act

            _service.ActualizarViaje(1, viajeDto);

            // Assert

            _repository.Verify(d => d.Update(It.IsAny<Viaje>()), Times.Once());
        }

        [Fact]
        public void ActualizarViaje_NotFound()
        {
            // Arrange

            var viaje = new Viaje()
            {
                ViajeId = 1
            };

            _repository.Setup(x => x.FindBy<Viaje>(1)).Returns(viaje);

            var viajeDto = new ViajeDTO()
            {

            };

            // Act

            //_service.ActualizarViaje(1, viajeDto);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.ActualizarViaje(2, viajeDto));
        }

        [Fact]
        public void GetViaje()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var viaje = new Viaje()
            {
                ViajeId = 1
            };

            var response = new ViajeResponseDTO()
            {
                ViajeId = 1
            };

            _repository.Setup(x => x.FindBy<Viaje>(1)).Returns(viaje);

            // Act

            var result = _service.GetViajeById(1);

            // Assert

            Assert.Equal(result.ViajeId, response.ViajeId);
        }

        [Fact]
        public void GetViaje_NotFound()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var viaje = new Viaje()
            {
                ViajeId = 1
            };

            var response = new ViajeResponseDTO()
            {
                ViajeId = 1
            };

            _repository.Setup(x => x.FindBy<Viaje>(1)).Returns(viaje);

            // Act

            //var result = _service.GetViajeById(2);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.GetViajeById(2));
        }

        [Fact]
        public void GetViajes()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var viajeList = new List<Viaje>()
            {
                new Viaje()
                {
                    ViajeId = 1,
                }                
            };

            _repository.Setup(x => x.Traer<Viaje>()).Returns(viajeList);

            // Act

            var result = _service.GetAll();

            // Assert

            Assert.Single(result);
        }

        //[Fact]
        //public void DeleteCoordinador()
        //{
        //    var coordinador = new Coordinador()
        //    {
        //        Nombre = "Nombre"
        //    };

        //    _repository.Setup(x => x.FindBy<Coordinador>(1)).Returns(coordinador);

        //    var viajes = new List<Viaje>();

        //    _repository.Setup(x => x.Traer<Viaje>()).Returns(viajes);

        //    // Act

        //    _service.DeleteCoordinadorById(1);

        //    // Assert

        //    return;
        //}
    }
}