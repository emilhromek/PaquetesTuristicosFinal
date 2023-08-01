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
    public class HotelTest
    {

        public HotelTest()
        {
        }

        [Fact]
        public void GetHotel_ReturnsOK()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var hotel = new Hotel()
            {
                Id = 1,
                DestinoId = 1,
            };
            var destino = new Destino()
            {
                Id = 1,
            };

            queriesRepository.Setup(x => x.EncontrarPor<Hotel>(1)).Returns(hotel);
            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            var hotelService = new HotelService(commandsRepository.Object, queriesRepository.Object);

            var output = new HotelOutDto() // esperado
            {
                Id = 1,
            };

            // Act

            var result = hotelService.GetHotelId(1);

            // Assert

            Assert.Equal(((HotelOutDto) result).Id, output.Id);

        }

        [Fact]
        public void GetHotel_ReturnsNotFound() // revisar
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var hotel = new Hotel()
            {
                Id = 1,
                DestinoId = 1,
            };
            var destino = new Destino()
            {
                Id = 1,
            };

            queriesRepository.Setup(x => x.EncontrarPor<Hotel>(1)).Returns(hotel);
            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            var hotelService = new HotelService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "NOT_FOUND"
            };

            // Act

            var result = hotelService.GetHotelId(2);

            // Assert

            Assert.Equal(response.Code, ((Response)result).Code);

        }

        [Fact]
        public void PostHotel_ReturnsOk()
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
                Id = 1,
            };

            var itemsInserted = new List<Hotel>();

            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            commandsRepository.Setup(i => i.Agregar(It.IsAny<Hotel>()))
            .Callback((Hotel item) => itemsInserted.Add(item));

            var hotelService = new HotelService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "OK"
            };

            // Act

            var result = hotelService.PostHotel(hotelDto);

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
        public void BorrarHotel_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var hotel = new Hotel()
            {
                Id = 1,
            };

            var paquetes = new List<Paquete>();
            var paquetesExcursiones = new List<PaqueteExcursion>();
            var reserva = new List<Reserva>();

            queriesRepository.Setup(x => x.EncontrarPor<Hotel>(1)).Returns(hotel);
            queriesRepository.Setup(x => x.Traer<Paquete>()).Returns(paquetes);
            queriesRepository.Setup(x => x.Traer<PaqueteExcursion>()).Returns(paquetesExcursiones);
            queriesRepository.Setup(x => x.Traer<Reserva>()).Returns(reserva);

            commandsRepository.Setup(x => x.BorrarPor<Destino>(1));

            var hotelService = new HotelService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "OK"
            };

            // Act

            var result = hotelService.BorrarHotel(1);

            // Assert

            Assert.Equal(response.Code, result.Code);
        }

        [Fact]
        public void BorrarHotel_ReturnsNotFound()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var hotel = new Hotel()
            {
                Id = 2,
            };

            var paquetes = new List<Paquete>();
            var paquetesExcursiones = new List<PaqueteExcursion>();
            var reserva = new List<Reserva>();

            queriesRepository.Setup(x => x.EncontrarPor<Hotel>(2)).Returns(hotel);
            queriesRepository.Setup(x => x.Traer<Paquete>()).Returns(paquetes);
            queriesRepository.Setup(x => x.Traer<PaqueteExcursion>()).Returns(paquetesExcursiones);
            queriesRepository.Setup(x => x.Traer<Reserva>()).Returns(reserva);

            commandsRepository.Setup(x => x.BorrarPor<Destino>(1));

            var hotelService = new HotelService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "NOT_FOUND"
            };

            // Act

            var result = hotelService.BorrarHotel(1);

            // Assert

            Assert.Equal(response.Code, result.Code);
        }

        [Fact]
        public void ActualizarHotel_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var destino = new Destino()
            {
                Id = 1,
                Descripcion = "Descripcion"
            };

            var hotel = new Hotel()
            {
                Id = 1,
            };

            var hotelUpdateDto = new HotelDto()
            {
                DestinoId = 1,
                Marca = "Marca update"
            };

            queriesRepository.Setup(x => x.EncontrarPor<Hotel>(1)).Returns(hotel);
            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            commandsRepository.Setup(x => x.ActualizarPor<Hotel>(1));

            var hotelService = new HotelService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "UPDATED"
            };

            // Act

            var result = hotelService.ActualizarHotel(1, hotelUpdateDto);

            // Assert

            Assert.Equal(response.Code, result.Code);
            commandsRepository.Verify(d => d.Actualizar(It.IsAny<Hotel>()), Times.Once());
        }

        [Fact]
        public void ActualizarHotel_ReturnsNotFound()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var destino = new Destino()
            {
                Id = 1,
                Descripcion = "Descripcion"
            };

            var hotel = new Hotel()
            {
                Id = 1,
            };

            var hotelUpdateDto = new HotelDto()
            {
                DestinoId = 1,
                Marca = "Marca update"
            };

            queriesRepository.Setup(x => x.EncontrarPor<Hotel>(1)).Returns(hotel);
            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            commandsRepository.Setup(x => x.ActualizarPor<Hotel>(2));

            var hotelService = new HotelService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "NOT_FOUND"
            };

            // Act

            var result = hotelService.ActualizarHotel(2, hotelUpdateDto);

            // Assert

            Assert.Equal(response.Code, result.Code);
            commandsRepository.Verify(d => d.Actualizar(It.IsAny<Hotel>()), Times.Never());
        }

        [Fact]
        public void GetHoteles_ReturnsOK()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var hoteles = new List<Hotel>()
            {
                new Hotel()
                {
                    Id = 1,
                    DestinoId = 1
                },
            };

            var destino = new Destino()
            {
                Id = 1,
            };

            queriesRepository.Setup(x => x.Traer<Hotel>()).Returns(hoteles);

            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            var hotelService = new HotelService(commandsRepository.Object, queriesRepository.Object);

            // Act

            var result = hotelService.GetHoteles("");

            // Assert

            Assert.Single(((List<HotelOutDto>)result));
        }
    }
}