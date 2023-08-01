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
    public class ComentarioTest
    {

        public ComentarioTest()
        {
        }
        

        [Fact]
        public void PostComentario_ReturnsOk()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var comentarioDto = new ComentarioDestinoDto()
            {
                DestinoId = 1,
                Mensaje = "", // recordatorio: si se hace un check de largo de mensaje, escribir aunque sea un mensaje vacio
            };

            var itemsInserted = new List<ComentarioDestino>();

            var destino = new Destino()
            {
                Id = 1,
            };

            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            commandsRepository.Setup(i => i.Agregar(It.IsAny<ComentarioDestino>()))
           .Callback((ComentarioDestino item) => itemsInserted.Add(item));

            var comentarioService = new ComentarioDestinoService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "CREATED"
            };

            // Act

            var result = comentarioService.PostComentarioDestino(comentarioDto);

            // Assert

            Assert.Single(itemsInserted);
            Assert.Equal(response.Code, result.Code);

        }

        [Fact]
        public void PostComentario_ReturnsBadRequest_ComentarioSuperaLongitudEsperada()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var comentarioDto = new ComentarioDestinoDto()
            {
                DestinoId = 1,
                Mensaje = "--------------------" +
                "--------------------" +
                "--------------------" +
                "--------------------" +
                "--------------------" +
                "--------------------" +
                "--------------------" +
                "--------------------" +
                "--------------------" +
                "--------------------" +
                "--------------------" +
                "--------------------" +
                "--------------------", // recordatorio: si se hace un check de largo de mensaje, escribir aunque sea un mensaje vacio
            };

            var destino = new Destino()
            {
                Id = 1,
            };

            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            var hotelService = new ComentarioDestinoService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "BAD_REQUEST"
            };

            // Act

            var result = hotelService.PostComentarioDestino(comentarioDto);

            // Assert

            Assert.Equal(response.Code, result.Code);

        }

        [Fact]
        public void GetComentarios_ReturnsOK() // revisar
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var comentarios = new List<ComentarioDestino>()
            {
                new ComentarioDestino()
                {
                    Id = 1,
                    DestinoId = 1,
                    PasajeroId = 1,
                },
            };

            var destino = new Destino()
            {
                Id = 1,
            };

            queriesRepository.Setup(x => x.Traer<ComentarioDestino>()).Returns(comentarios);

            queriesRepository.Setup(x => x.EncontrarPor<Destino>(1)).Returns(destino);

            var comentarioService = new ComentarioDestinoService(commandsRepository.Object, queriesRepository.Object);

            // Act

            var result = comentarioService.GetComentariosDestino("1","1");

            // Assert

            Assert.Single(((List<ComentarioDestinoOutDto>)result));


        }
    }
}