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

namespace Tests
{
    public class TerminalTest
    {
        private readonly Mock<ITerminalRepository> _repository;
        private readonly TerminalService _service;
        public TerminalTest()
        {
            _repository = new Mock<ITerminalRepository>();
            _service = new TerminalService(_repository.Object);
        }

        [Fact]
        public void AddTerminalTest()
        {
            // Arrange

            var itemsInserted = new List<Terminal>();

            var terminalDTO = new TerminalDTO()
            {
                Nombre = "Nombre"
            };

            var dtoResponse = new TerminalResponseDTO()
            {
                Nombre = "Nombre"
            };

            _repository.Setup(i => i.Add(It.IsAny<Terminal>()))
            .Callback((Terminal item) => itemsInserted.Add(item));

            var service = new TerminalService(_repository.Object);

            // Act

            var result = service.AddTerminal(terminalDTO);

            // Assert

            Assert.Single(itemsInserted);
            Assert.Equal(result.Nombre, dtoResponse.Nombre);

        }

        [Fact]
        public void ActualizarTerminal()
        {
            // Arrange

            var terminal = new Terminal()
            {
                TerminalId = 1,
            };

            var terminalDto = new TerminalDTO()
            {
                Nombre = "Nombre",
            };

            _repository.Setup(x => x.FindBy<Terminal>(1)).Returns(terminal);

            // Act

            _service.ActualizarTerminal(1, terminalDto);

            // Assert

            _repository.Verify(d => d.Update(It.IsAny<Terminal>()), Times.Once());

        }

        [Fact]
        public void ActualizarTerminal_NotFound()
        {
            // Arrange

            var terminal = new Terminal()
            {
                TerminalId = 1,
            };

            var terminalDto = new TerminalDTO()
            {
                Nombre = "Nombre",
            };

            _repository.Setup(x => x.FindBy<Terminal>(1)).Returns(terminal);

            // Act

            //_service.ActualizarTerminal(2, terminalDto);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.ActualizarTerminal(2, terminalDto));

        }

        [Fact]
        public void GetTerminal()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var terminal = new Terminal()
            {
                TerminalId = 1
            };

            var response = new TerminalResponseDTO()
            {
                TerminalId = 1
            };

            _repository.Setup(x => x.FindBy<Terminal>(1)).Returns(terminal);

            // Act

            var result = _service.GetTerminalById(1);

            // Assert

            Assert.Equal(result.TerminalId, response.TerminalId);
        }

        [Fact]
        public void GetTerminal_NotFound()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var terminal = new Terminal()
            {
                TerminalId = 1
            };

            var response = new TerminalResponseDTO()
            {
                TerminalId = 1
            };

            _repository.Setup(x => x.FindBy<Terminal>(1)).Returns(terminal);

            // Act

            //var result = _service.GetTerminalById(2);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.GetTerminalById(2));
        }

        [Fact]
        public void GetTerminales()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var terminalList = new List<Terminal>()
            {
                new Terminal()
                {
                    TerminalId = 1,
                }                
            };

            _repository.Setup(x => x.Traer<Terminal>()).Returns(terminalList);

            // Act

            var result = _service.GetAll();

            // Assert

            Assert.Single(result);
        }

        [Fact]
        public void DeleteTerminal()
        {
            var itemsInserted = new List<Terminal>();

            itemsInserted.Add(new Terminal()
            {
                TerminalId = 1
            });

            _repository.Setup(x => x.FindBy<Terminal>(1)).Returns(itemsInserted.Find(x => x.TerminalId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Terminal>())).Callback<Terminal>((entity) => itemsInserted.Remove(entity));

            // Act

            _service.DeleteTerminalById(1);

            // Assert

            Assert.Empty(itemsInserted);
            _repository.Verify(d => d.Delete(It.IsAny<Terminal>()), Times.Once());
        }

        [Fact]
        public void DeleteTerminal_NotFound()
        {
            var itemsInserted = new List<Terminal>();

            itemsInserted.Add(new Terminal()
            {
                TerminalId = 1
            });

            _repository.Setup(x => x.FindBy<Terminal>(1)).Returns(itemsInserted.Find(x => x.TerminalId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Terminal>())).Callback<Terminal>((entity) => itemsInserted.Remove(entity));

            // Act

            //_service.DeleteTerminalById(1);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.DeleteTerminalById(2));
        }
    }
}