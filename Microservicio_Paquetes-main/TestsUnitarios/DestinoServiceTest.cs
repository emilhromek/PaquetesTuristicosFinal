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
    public class DestinoServiceTest
    {

        public DestinoServiceTest()
        {
        }

        [Fact]
        public void GetDestino_ReturnsOK()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var id = 1;
            var destino = new Destino()
            {
                Id = id,
            };
            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            var destinoService = new DestinoService(commandsRepository.Object, queriesRepository.Object);

            var output = new DestinoOutDto() // esperado
            {
                Id = 1,
            };

            // Act

            var result = destinoService.GetDestinoId(1);

            // Assert

            Assert.Equal(((DestinoOutDto) result).Id, output.Id);

        }

        [Fact]
        public void GetDestino_ReturnsNotFound() // revisar
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var id = 1;
            var destino = new Destino()
            {
                Id = id,
            };
            queriesRepository.Setup(x => x.EncontrarPor<Destino>(2)).Returns(destino);

            var destinoService = new DestinoService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "NOT_FOUND"
            };

            // Act

            var result = destinoService.GetDestinoId(1);

            // Assert

            Assert.Equal(response.Code, ((Response)result).Code);

        }

        [Fact]
        public void PostDestino_ReturnsOk()
        {
            // Arrange

            var itemsInserted = new List<Destino>();

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var destinoDto = new DestinoDto()
            {
                Descripcion = "Descripcion"
            };

            commandsRepository.Setup(i => i.Agregar(It.IsAny<Destino>()))
            .Callback((Destino item) => itemsInserted.Add(item));

            var destinoService = new DestinoService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "CREATED"
            };

            // Act

            var result = destinoService.PostDestino(destinoDto);

            // Assert

            Assert.Equal(response.Code, result.Code);
            Assert.Single(itemsInserted);

        }

        [Fact]
        public void BorrarDestino_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var itemsInserted = new List<Destino>();

            itemsInserted.Add(new Destino()
            {
                Id = 1
            });

            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(itemsInserted.Find(x => x.Id == 1));

            commandsRepository.Setup(m => m.Borrar(It.IsAny<Destino>())).Callback<Destino>((entity) => itemsInserted.Remove(entity));

            var destinoService = new DestinoService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "OK"
            };

            // Act

            var result = destinoService.BorrarDestino(1);

            // Assert

            Assert.Equal(response.Code, result.Code);
            Assert.Empty(itemsInserted);
        }

        [Fact]
        public void BorrarDestino_ReturnsNotFound()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var destino = new Destino()
            {
                Id = 1,
            };

            commandsRepository.Setup(x => x.BorrarPor<Destino>(2));

            var destinoService = new DestinoService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "NOT_FOUND"
            };

            // Act

            var result = destinoService.BorrarDestino(1);

            // Assert

            Assert.Equal(response.Code, result.Code);
        }

        [Fact]
        public void ActualizarDestino_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var itemsInserted = new List<Destino>();

            itemsInserted.Add(new Destino()
            {
                Id = 1,
                Descripcion = "Descripcion"
            });

            var destinoUpdateDto = new DestinoDto()
            {
                Descripcion = "Descripcion update"
            };

            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(itemsInserted.Find(x => x.Id == 1));

            var destinoService = new DestinoService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "UPDATED"
            };

            // Act

            var result = destinoService.ActualizarDestino(1, destinoUpdateDto);

            // Assert

            Assert.Equal(response.Code, result.Code);
            commandsRepository.Verify(d => d.Actualizar(It.IsAny<Destino>()), Times.Once());
        }

        [Fact]
        public void ActualizarDestino_ReturnsNotFound()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();

            var destino = new Destino()
            {
                Id = 1,
                Descripcion = "Descripcion"
            };

            var destinoUpdateDto = new DestinoDto()
            {
                Descripcion = "Descripcion update"
            };

            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            commandsRepository.Setup(x => x.ActualizarPor<Destino>(2));

            var destinoService = new DestinoService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "NOT_FOUND"
            };

            // Act

            var result = destinoService.ActualizarDestino(2, destinoUpdateDto);

            // Assert

            Assert.Equal(response.Code, result.Code);
        }

        [Fact]
        public void GetDestinos_ReturnsOK()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var destinos = new List<Destino>()
            {
                new Destino()
                {
                    Id = 1,
                },
            };
            queriesRepository.Setup(x => x.Traer<Destino>()).Returns(destinos);

            var destinoService = new DestinoService(commandsRepository.Object, queriesRepository.Object);

            var output = new DestinoOutDto() // esperado
            {
                Id = 1,
            };

            // Act

            var result = destinoService.GetDestinos();

            // Assert

            Assert.Single(((List<DestinoOutDto>)result));


        }
    }
}