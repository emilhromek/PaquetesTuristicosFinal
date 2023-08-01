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
    public class PaqueteTest
    {

        public PaqueteTest()
        {
        }

        [Fact]
        public void GetPaquete_ReturnsOK()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var paquete = new Paquete()
            {
                Id = 1,
                DestinoId = 1,
                HotelId = 1
            };
            var destino = new Destino()
            {
                Id = 1,
            };
            var hotel = new Hotel()
            {
                Id = 1,
            };
            var paqueteExcursionList = new List<PaqueteExcursion>()
            {

            };

            queriesRepository.Setup(x => x.EncontrarPor<Paquete>(1)).Returns(paquete);
            queriesRepository.Setup(x => x.EncontrarPor<Hotel>(1)).Returns(hotel);
            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);
            queriesRepository.Setup(x => x.Traer<PaqueteExcursion>()).Returns(paqueteExcursionList);

            var paqueteService = new PaqueteService(commandsRepository.Object, queriesRepository.Object);

            var output = new PaqueteOutDto() // esperado
            {
                Id = 1,
            };

            // Act

            var result = paqueteService.GetPaqueteId(1);

            // Assert

            Assert.Equal(((PaqueteOutDto) result).Id, output.Id);

        }

        [Fact]
        public void GetPaquete_ReturnsNotFound() // revisar
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var paquete = new Paquete()
            {
                Id = 1,
                DestinoId = 1,
                HotelId = 1
            };
            var destino = new Destino()
            {
                Id = 1,
            };
            var hotel = new Hotel()
            {
                Id = 1,
            };
            var paqueteExcursionList = new List<PaqueteExcursion>()
            {

            };

            queriesRepository.Setup(x => x.EncontrarPor<Paquete>(1)).Returns(paquete);
            queriesRepository.Setup(x => x.EncontrarPor<Hotel>(1)).Returns(hotel);
            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);
            queriesRepository.Setup(x => x.Traer<PaqueteExcursion>()).Returns(paqueteExcursionList);

            var paqueteService = new PaqueteService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "NOT_FOUND"
            };

            // Act

            var result = paqueteService.GetPaqueteId(2);

            // Assert

            Assert.Equal(response.Code, ((Response)result).Code);

        }

        [Fact]
        public void PostPaquete_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var paqueteDto = new PaqueteDto()
            {
                DestinoId = 1,
                Descuento = 10,
                Excursiones = {},
            };

            var destino = new Destino()
            {
                Id = 1,
            };

            var itemsInserted = new List<Paquete>();

            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);
            commandsRepository.Setup(i => i.Agregar(It.IsAny<Paquete>()))
            .Callback((Paquete item) => itemsInserted.Add(item));

            var paqueteService = new PaqueteService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "OK"
            };

            // Act

            var result = paqueteService.PostPaquete(paqueteDto);

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
        public void BorrarPaquete_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var itemsInserted = new List<Paquete>();

            itemsInserted.Add(new Paquete()
            {
                Id = 1
            });
            var paquetesExcursiones = new List<PaqueteExcursion>();
            var reserva = new List<Reserva>();

            queriesRepository.Setup(x => x.EncontrarPor<Paquete>(1)).Returns(itemsInserted.Find(x => x.Id == 1));
            queriesRepository.Setup(x => x.Traer<PaqueteExcursion>()).Returns(paquetesExcursiones);
            queriesRepository.Setup(x => x.Traer<Reserva>()).Returns(reserva);

            commandsRepository.Setup(m => m.Borrar(It.IsAny<Paquete>())).Callback<Paquete>((entity) => itemsInserted.Remove(entity));

            var paqueteService = new PaqueteService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "OK"
            };

            // Act

            var result = paqueteService.deletePaqueteId(1);

            // Assert

            Assert.Equal(response.Code, result.Code);
        }

        [Fact]
        public void BorrarPaquete_ReturnsNotFound()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var paquete = new Paquete()
            {
                Id = 1,
            };

            var paquetes = new List<Paquete>();
            var paquetesExcursiones = new List<PaqueteExcursion>();
            var reserva = new List<Reserva>();

            queriesRepository.Setup(x => x.EncontrarPor<Paquete>(1)).Returns(paquete);
            queriesRepository.Setup(x => x.Traer<Paquete>()).Returns(paquetes);
            queriesRepository.Setup(x => x.Traer<PaqueteExcursion>()).Returns(paquetesExcursiones);
            queriesRepository.Setup(x => x.Traer<Reserva>()).Returns(reserva);

            commandsRepository.Setup(x => x.BorrarPor<Destino>(1));

            var paqueteService = new PaqueteService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "NOT_FOUND"
            };

            // Act

            var result = paqueteService.deletePaqueteId(2);

            // Assert

            Assert.Equal(response.Code, result.Code);
        }

        [Fact]
        public void GetPaquetes_ReturnsOK()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var paquetes = new List<Paquete>
            {
                new Paquete()
                {
                    Id = 1,
                    DestinoId = 1,
                    HotelId = 1
                }
                
            };
            var destino = new Destino()
            {
                Id = 1,
            };
            var hotel = new Hotel()
            {
                Id = 1,
            };
            var paqueteExcursionList = new List<PaqueteExcursion>()
            {

            };

            queriesRepository.Setup(x => x.Traer<Paquete>()).Returns(paquetes);
            queriesRepository.Setup(x => x.EncontrarPor<Hotel>(1)).Returns(hotel);
            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);
            queriesRepository.Setup(x => x.Traer<PaqueteExcursion>()).Returns(paqueteExcursionList);

            var paqueteService = new PaqueteService(commandsRepository.Object, queriesRepository.Object);

            var output = new PaqueteOutDto() // esperado
            {
                Id = 1,
            };

            // Act

            var result = paqueteService.GetPaquetes("");

            // Assert

            Assert.Single((List<object>)result);

        }
    }
}