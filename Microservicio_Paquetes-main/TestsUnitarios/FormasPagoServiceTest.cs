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
    public class FormasPagoServiceTest
    {

        public FormasPagoServiceTest()
        {
        }

        [Fact]
        public void GetFormaPago_ReturnsOK()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var id = 1;
            var formaPago = new FormaPago()
            {
                Id = id,
            };
            queriesRepository.Setup(x => x.EncontrarPor<FormaPago>(1)).Returns(formaPago);

            var formaPagoService = new FormaPagoService(commandsRepository.Object, queriesRepository.Object);

            var output = new FormaPagoOutDto() // esperado
            {
                Id = 1,
            };

            // Act

            var result = formaPagoService.GetFormaPagoId(1);

            // Assert

            Assert.Equal(((FormaPagoOutDto) result).Id, output.Id);

        }

        [Fact]
        public void GetFormaPago_ReturnsNotFound() // revisar
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var id = 1;
            var formaPago = new FormaPago()
            {
                Id = id,
            };
            queriesRepository.Setup(x => x.EncontrarPor<FormaPago>(2)).Returns(formaPago);

            var formaPagoService = new FormaPagoService(commandsRepository.Object, queriesRepository.Object);

            var response = new Response()
            {
                Code = "NOT_FOUND"
            };

            // Act

            var result = formaPagoService.GetFormaPagoId(1);

            // Assert

            Assert.Equal(response.Code, ((Response)result).Code);

        }

        [Fact]
        public void GetFormasPago_ReturnsOK()
        {
            // Arrange

            var commandsRepository = new Mock<ICommands>();
            var queriesRepository = new Mock<IQueries>();
            var formasPago = new List<FormaPago>()
            {
                new FormaPago()
                {
                    Id = 1,
                },
            };
            queriesRepository.Setup(x => x.Traer<FormaPago>()).Returns(formasPago);

            var destinoService = new FormaPagoService(commandsRepository.Object, queriesRepository.Object);

            var output = new DestinoOutDto() // esperado
            {
                Id = 1,
            };

            // Act

            var result = destinoService.GetFormasPago();

            // Assert

            Assert.Single(((List<FormaPagoOutDto>)result));


        }
    }
}