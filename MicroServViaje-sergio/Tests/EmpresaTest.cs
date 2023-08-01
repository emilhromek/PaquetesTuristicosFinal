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

namespace Tests
{
    public class EmpresaTest
    {
        private readonly Mock<IEmpresaRepository> _repository;
        private readonly EmpresaService _service;
        public EmpresaTest()
        {
            _repository = new Mock<IEmpresaRepository>();
            _service = new EmpresaService(_repository.Object);
        }

        [Fact]
        public void AddEmpresaTest()
        {
            // Arrange

            var itemsInserted = new List<Empresa>();

            var empresaDTO = new EmpresaDTO()
            {
                Nombre = "Nombre"
            };

            var dtoResponse = new EmpresaResponseDTO()
            {
                Nombre = "Nombre"
            };

            _repository.Setup(i => i.Add(It.IsAny<Empresa>()))
            .Callback((Empresa item) => itemsInserted.Add(item));

            var service = new EmpresaService(_repository.Object);

            // Act

            var result = service.AddEmpresa(empresaDTO);

            // Assert

            Assert.Single(itemsInserted);
            Assert.Equal(result.Nombre, dtoResponse.Nombre);

        }

        [Fact]
        public void ActualizarEmpresa()
        {
            // Arrange

            var empresa = new Empresa()
            {
                EmpresaId = 1,
            };

            _repository.Setup(x => x.FindBy<Empresa>(1)).Returns(empresa);

            var empresaDto = new EmpresaDTO()
            {
                Nombre = "Nombre update",
            };

            // Act

            _service.ActualizarEmpresa(1, empresaDto);

            // Assert

            _repository.Verify(d => d.Update(It.IsAny<Empresa>()), Times.Once());

        }

        [Fact]
        public void ActualizarEmpresa_NotFound()
        {
            // Arrange

            var empresa = new Empresa()
            {
                EmpresaId = 1,
            };

            _repository.Setup(x => x.FindBy<Empresa>(1)).Returns(empresa);

            var empresaDto = new EmpresaDTO()
            {
                Nombre = "Nombre update",
            };

            // Act

            //_service.ActualizarEmpresa(1, empresaDto);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.ActualizarEmpresa(2, empresaDto));

        }

        [Fact]
        public void GetEmpresa()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var empresa = new Empresa()
            {
                EmpresaId = 1
            };

            var response = new EmpresaResponseDTO()
            {
                EmpresaId = 1
            };

            _repository.Setup(x => x.FindBy<Empresa>(1)).Returns(empresa);

            // Act

            var result = _service.GetEmpresaById(1);

            // Assert

            Assert.Equal(result.Nombre, response.Nombre);
        }

        [Fact]
        public void GetEmpresa_NotFound()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var empresa = new Empresa()
            {
                EmpresaId = 1
            };

            var response = new EmpresaResponseDTO()
            {
                EmpresaId = 1
            };

            _repository.Setup(x => x.FindBy<Empresa>(1)).Returns(empresa);

            // Act

            //var result = _service.GetEmpresaById(2);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.GetEmpresaById(2));
        }

        [Fact]
        public void GetEmpresas()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var empresaList = new List<Empresa>()
            {
                new Empresa()
                {
                    EmpresaId = 1,
                }                
            };

            _repository.Setup(x => x.Traer<Empresa>()).Returns(empresaList);

            // Act

            var result = _service.GetAll();

            // Assert

            Assert.Single(result);
        }

        [Fact]
        public void DeleteEmpresa()
        {
            var itemsInserted = new List<Empresa>();

            itemsInserted.Add(new Empresa()
            {
                EmpresaId = 1
            });

            _repository.Setup(x => x.FindBy<Empresa>(1)).Returns(itemsInserted.Find(x => x.EmpresaId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Empresa>())).Callback<Empresa>((entity) => itemsInserted.Remove(entity));

            // Act

            _service.DeleteEmpresaById(1);

            // Assert

            Assert.Empty(itemsInserted);
        }

        [Fact]
        public void DeleteEmpresa_NotFound()
        {
            var itemsInserted = new List<Empresa>();

            itemsInserted.Add(new Empresa()
            {
                EmpresaId = 1
            });

            _repository.Setup(x => x.FindBy<Empresa>(1)).Returns(itemsInserted.Find(x => x.EmpresaId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Empresa>())).Callback<Empresa>((entity) => itemsInserted.Remove(entity));

            // Act

            //_service.DeleteEmpresaById(2);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.DeleteEmpresaById(2));
        }
    }
}