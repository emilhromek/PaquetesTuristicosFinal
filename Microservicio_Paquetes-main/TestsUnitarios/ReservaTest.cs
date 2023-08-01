using Microservicio_Paquetes.AccessData;
using Microservicio_Paquetes.Application.Services;
using Microservicio_Paquetes.Domain.Commands;
using Microservicio_Paquetes.Domain.DTO;
using Microservicio_Paquetes.Domain.Entities;
using Microservicio_Paquetes.Domain.Queries;
using Microservicio_Paquetes.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestsUnitarios
{
    public class ReservaTest
    {

        public ReservaTest()
        {
        }

        [Fact]
        public void GetReserva_ReturnsOK()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var reserva = new Reserva()
            {
                Id = 1,
            };

            var reservarExcursion = new List<ReservaExcursion>();

            queriesRepository.Setup(x => x.EncontrarPor<Reserva>(1)).Returns(reserva);
            queriesRepository.Setup(x => x.Traer<ReservaExcursion>()).Returns(reservarExcursion);

            var reservaService = new ReservaService(commandsRepository.Object, queriesRepository.Object);

            var output = new ReservaOutDto() // esperado
            {
                Id = 1,
            };

            // Act

            var result = reservaService.GetReservaId(1);

            // Assert

            Assert.Equal(((ReservaOutDto) result).Id, output.Id);

        }

        [Fact]
        public void GetReserva_ReturnsNotFound() // revisar
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var reserva = new Reserva()
            {
                Id = 1,
            };

            var reservarExcursion = new List<ReservaExcursion>();

            queriesRepository.Setup(x => x.EncontrarPor<Reserva>(1)).Returns(reserva);
            queriesRepository.Setup(x => x.Traer<ReservaExcursion>()).Returns(reservarExcursion);

            var reservaService = new ReservaService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "NOT_FOUND"
            };

            // Act

            var result = reservaService.GetReservaId(2);

            // Assert

            Assert.Equal(response.Code, ((Response)result).Code);

        }

        [Fact]
        public void PostReserva_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var reservaDto = new ReservaDto()
            {
                Pasajeros = 1,
                FormaPagoId = 1,
                PaqueteId = 1,
                Pagado = true,
                PasajeroId = 1,
                ListaExcursiones = {}
            };

            var formaPago = new FormaPago()
            {
                Id = 1,
            };

            var paquete = new Paquete()
            {
                Id = 1,
                Precio = 1,
            };

            var itemsInserted = new List<Reserva>();

            queriesRepository.Setup(x => x.EncontrarPor<FormaPago>(1)).Returns(formaPago);
            queriesRepository.Setup(x => x.EncontrarPor<Paquete>(1)).Returns(paquete);

            commandsRepository.Setup(i => i.Agregar(It.IsAny<Reserva>()))
            .Callback((Reserva item) => itemsInserted.Add(item));

            var reservaService = new ReservaService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "OK"
            };

            // Act

            var result = reservaService.PostReserva(reservaDto);

            // Assert

            Assert.Single(itemsInserted);
            Assert.Equal(response.Code, result.Code);

        }

        [Fact]
        public void PostHotel_ReturnsBadRequest_DestinoNoExiste()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var hotelDto = new HotelDto()
            {
                DestinoId = 1
            };

            var destino = new Destino()
            {
                Id = 2,
            };

            queriesRepository.Setup(x => x.EncontrarPor<Destino>(2)).Returns(destino);

            var hotelService = new HotelService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "BAD_REQUEST"
            };

            // Act

            var result = hotelService.PostHotel(hotelDto);

            // Assert

            Assert.Equal(response.Code, result.Code);

        }

        [Fact]
        public void PatchReservaPagado_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var reserva = new Reserva()
            {
                Id = 1,
                Pagado = false
            };

            queriesRepository.Setup(x => x.EncontrarPor<Reserva>(1)).Returns(reserva);

            var reservaService = new ReservaService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "UPDATED"
            };

            // Act

            var result = reservaService.PatchReservaPagadoId(1);

            // Assert

            Assert.Equal(response.Code, result.Code);
            commandsRepository.Verify(d => d.Actualizar(It.IsAny<Reserva>()), Times.Once());
        }

        [Fact]
        public void PatchReservaGrupo_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var reserva = new Reserva()
            {
                Id = 1,
            };

            queriesRepository.Setup(x => x.EncontrarPor<Reserva>(1)).Returns(reserva);

            var reservaService = new ReservaService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "UPDATED"
            };

            // Act

            var result = reservaService.PatchReservaGrupoId(1,1);

            // Assert

            Assert.Equal(response.Code, result.Code);
            commandsRepository.Verify(d => d.Actualizar(It.IsAny<Reserva>()), Times.Once());
        }

        [Fact]
        public void UnpatchReservaGrupo_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var reserva = new Reserva()
            {
                Id = 1,
            };

            queriesRepository.Setup(x => x.EncontrarPor<Reserva>(1)).Returns(reserva);

            var reservaService = new ReservaService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "UPDATED"
            };

            // Act

            var result = reservaService.UnpatchReservaGrupoId(1);
            commandsRepository.Verify(d => d.Actualizar(It.IsAny<Reserva>()), Times.Once());

            // Assert

            Assert.Equal(response.Code, result.Code);
        }

        [Fact]
        public void PatchReservaViaje_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var reserva = new Reserva()
            {
                Id = 1,
            };

            queriesRepository.Setup(x => x.EncontrarPor<Reserva>(1)).Returns(reserva);

            var reservaService = new ReservaService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "UPDATE"
            };

            // Act

            var result = reservaService.PatchReservaViajeId(1, 1);

            // Assert

            Assert.Equal(response.Code, result.Code);
            commandsRepository.Verify(d => d.Actualizar(It.IsAny<Reserva>()), Times.Once());
        }

        [Fact]
        public void UnpatchReservaViaje_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var reserva = new Reserva()
            {
                Id = 1,
            };

            queriesRepository.Setup(x => x.EncontrarPor<Reserva>(1)).Returns(reserva);

            var reservaService = new ReservaService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "UPDATED"
            };

            // Act

            var result = reservaService.UnpatchReservaViajeId(1);

            // Assert

            Assert.Equal(response.Code, result.Code);
            commandsRepository.Verify(d => d.Actualizar(It.IsAny<Reserva>()), Times.Once());
        }

        [Fact]
        public void AsignarViajeAReservasSegunGrupo_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var reservas = new List<Reserva>()
            {
                new Reserva()
                {
                    Id = 1,
                    GrupoId = 1
                }                
            };

            queriesRepository.Setup(x => x.Traer<Reserva>()).Returns(reservas);

            var reservaService = new ReservaService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "OK"
            };

            // Act

            var result = reservaService.AsignarViajeAReservasSegunGrupo(1, 1);

            // Assert

            Assert.Equal(response.Code, result.Code);
            commandsRepository.Verify(d => d.Actualizar(It.IsAny<Reserva>()), Times.Once());
        }

        [Fact]
        public void DesasignarViajeAReservasSegunGrupo_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var reservas = new List<Reserva>()
            {
                new Reserva()
                {
                    Id = 1,
                    GrupoId = 1,
                    ViajeId = 1
                }
            };

            queriesRepository.Setup(x => x.Traer<Reserva>()).Returns(reservas);

            var reservaService = new ReservaService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "OK"
            };

            // Act

            var result = reservaService.DesasignarViajeAReservasSegunGrupo(1);

            // Assert

            Assert.Equal(response.Code, result.Code);
            commandsRepository.Verify(d => d.Actualizar(It.IsAny<Reserva>()), Times.Once());
        }
    }
}