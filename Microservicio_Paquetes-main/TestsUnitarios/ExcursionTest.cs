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
    public class ExcursionTest
    {

        public ExcursionTest()
        {
        }

        [Fact]
        public void GetExcursion_ReturnsOK()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var excursion = new Excursion()
            {
                Id = 1,
                DestinoId = 1,
            };
            var destino = new Destino()
            {
                Id = 1,
            };

            queriesRepository.Setup(x => x.EncontrarPor<Excursion>(1)).Returns(excursion);
            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            var excursionService = new ExcursionService(commandsRepository.Object, queriesRepository.Object);

            var output = new ExcursionOutDto() // esperado
            {
                Id = 1,
            };

            // Act

            var result = excursionService.GetExcursionId(1);

            // Assert

            Assert.Equal(((ExcursionOutDto) result).Id, output.Id);

        }

        [Fact]
        public void GetExcursion_ReturnsNotFound()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var excursion = new Excursion()
            {
                Id = 2,
                DestinoId = 1,
            };
            var destino = new Destino()
            {
                Id = 1,
            };

            queriesRepository.Setup(x => x.EncontrarPor<Excursion>(2)).Returns(excursion);
            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            var excursionService = new ExcursionService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "NOT_FOUND"
            };

            // Act

            var result = excursionService.BorrarExcursion(1);

            // Assert

            Assert.Equal(response.Code, result.Code);

        }
    

        [Fact]
        public void PostExcursion_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var excursionDto = new ExcursionDto()
            {
                DestinoId = 1,
                Titulo = "Titulo",
                Descripcion = "Descripcion"
            };

            var itemsInserted = new List<Excursion>();

            var destino = new Destino()
            {
                Id = 1,
            };

            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            commandsRepository.Setup(i => i.Agregar(It.IsAny<Excursion>()))
            .Callback((Excursion item) => itemsInserted.Add(item));

            var excursionService = new ExcursionService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "CREATED"
            };

            // Act

            var result = excursionService.PostExcursion(excursionDto);

            // Assert

            Assert.Single(itemsInserted);
            Assert.Equal(response.Code, result.Code);

        }

        [Fact]
        public void PostExcursion_ReturnsBadRequest_DestinoNoExiste()
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
        public void BorrarExcursion_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var itemsInserted = new List<Excursion>();

            itemsInserted.Add(new Excursion()
            {
                Id = 1
            });

            queriesRepository.Setup(x => x.EncontrarPor<Excursion>(1)).Returns(itemsInserted.Find(x => x.Id == 1));

            commandsRepository.Setup(m => m.Borrar(It.IsAny<Excursion>())).Callback<Excursion>((entity) => itemsInserted.Remove(entity));

            var excursionService = new ExcursionService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "OK"
            };

            // Act

            var result = excursionService.BorrarExcursion(1);

            // Assert

            Assert.Empty(itemsInserted);
            Assert.Equal(response.Code, result.Code);
        }

        [Fact]
        public void BorrarExcursion_ReturnsNotFound()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var excursion = new Excursion()
            {
                Id = 2,
            };

            queriesRepository.Setup(x => x.EncontrarPor<Excursion>(2)).Returns(excursion);

            commandsRepository.Setup(x => x.BorrarPor<Destino>(1));

            var excursionService = new ExcursionService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "NOT_FOUND"
            };

            // Act

            var result = excursionService.BorrarExcursion(1);

            // Assert

            Assert.Equal(response.Code, result.Code);
        }

        [Fact]
        public void ActualizarExcursion_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var destino = new Destino()
            {
                Id = 1,
                Descripcion = "Descripcion"
            };

            var excursion = new Excursion()
            {
                Id = 1,
            };

            var excursionUpdateDto = new ExcursionDto()
            {
                DestinoId = 1,
                Descripcion = "Descripcion update"
            };

            queriesRepository.Setup(x => x.EncontrarPor<Excursion>(1)).Returns(excursion);
            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            commandsRepository.Setup(x => x.ActualizarPor<Excursion>(1));

            var excursionService = new ExcursionService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "UPDATED"
            };

            // Act

            var result = excursionService.ActualizarExcursion(1, excursionUpdateDto);

            // Assert

            Assert.Equal(response.Code, result.Code);
            commandsRepository.Verify(d => d.Actualizar(It.IsAny<Excursion>()), Times.Once());
        }

        [Fact]
        public void ActualizarExcursion_ReturnsNotFound()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var excursion = new Excursion()
            {
                Id = 2,
                DestinoId = 1,
            };

            var excursionUpdateDto = new ExcursionDto()
            {
                DestinoId = 1,
                Descripcion = "Descripcion update"
            };
            queriesRepository.Setup(x => x.EncontrarPor<Excursion>(2)).Returns(excursion); 

            var excursionService = new ExcursionService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "NOT_FOUND"
            };

            // Act

            var result = excursionService.ActualizarExcursion(1, excursionUpdateDto);

            // Assert

            Assert.Equal(response.Code, result.Code);
            commandsRepository.Verify(d => d.Actualizar(It.IsAny<Excursion>()), Times.Never());
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