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
using Turismo.Template.Domain.DTO.Grupo;

namespace Tests
{
    public class GrupoTest
    {
        private readonly Mock<IGrupoRepository> _repository;
        private readonly GrupoService _service;
        public GrupoTest()
        {
            _repository = new Mock<IGrupoRepository>();
            _service = new GrupoService(_repository.Object);
        }

        [Fact]
        public void AddGrupo()
        {
            // Arrange

            var itemsInserted = new List<Grupo>();

            var grupoDTO = new GrupoDTO()
            {
                TotalPasajeros = 50
            };

            var dtoResponse = new GrupoResponseDTO()
            {
                TotalPasajeros = 50
            };

            _repository.Setup(i => i.Add(It.IsAny<Grupo>()))
            .Callback((Grupo item) => itemsInserted.Add(item));

            var service = new GrupoService(_repository.Object);

            // Act

            var result = service.AddGrupo(grupoDTO);

            // Assert

            Assert.Single(itemsInserted);
            Assert.Equal(result.TotalPasajeros, dtoResponse.TotalPasajeros);

        }

        [Fact]
        public void PatchGrupo()
        {
            // Arrange

            var grupo = new Grupo()
            {
                GrupoId = 1
            };

            _repository.Setup(x => x.FindBy<Grupo>(1)).Returns(grupo);

            // Act

            _service.PatchGrupoViaje(1,1);

            // Assert

            _repository.Verify(d => d.Update(It.IsAny<Grupo>()), Times.Once());

        }

        [Fact]
        public void PatchGrupo_NotFound()
        {
            // Arrange

            var grupo = new Grupo()
            {
                GrupoId = 1
            };

            _repository.Setup(x => x.FindBy<Grupo>(1)).Returns(grupo);

            // Act

            //_service.PatchGrupoViaje(2, 1);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.PatchGrupoViaje(2, 1));

        }

        [Fact]
        public void UnpatchGrupo()
        {
            // Arrange

            var grupo = new Grupo()
            {
                GrupoId = 1
            };

            _repository.Setup(x => x.FindBy<Grupo>(1)).Returns(grupo);

            // Act

            _service.UnpatchGrupoViaje(1);

            // Assert

            _repository.Verify(d => d.Update(It.IsAny<Grupo>()), Times.Once());

        }

        [Fact]
        public void GetGrupo()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var grupo = new Grupo()
            {
                GrupoId = 1
            };

            var response = new GrupoResponseDTO()
            {
                GrupoId = 1
            };

            _repository.Setup(x => x.FindBy<Grupo>(1)).Returns(grupo);

            // Act

            var result = _service.GetGrupoById(1);

            // Assert

            Assert.Equal(result.GrupoId, response.GrupoId);
        }

        [Fact]
        public void GetGrupo_NotFound()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var grupo = new Grupo()
            {
                GrupoId = 1
            };

            var response = new GrupoResponseDTO()
            {
                GrupoId = 1
            };

            _repository.Setup(x => x.FindBy<Grupo>(1)).Returns(grupo);

            // Act

            //var result = _service.GetGrupoById(2);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.GetGrupoById(2));
        }

        [Fact]
        public void GetGrupos()
        {
            // Arrange

            //var repository = new Mock<IBusRepository>();
            //var service = new BusService(repository.Object);

            var grupoList = new List<Grupo>()
            {
                new Grupo()
                {
                    GrupoId = 1,
                }                
            };

            _repository.Setup(x => x.Traer<Grupo>()).Returns(grupoList);

            // Act

            var result = _service.GetAll();

            // Assert

            Assert.Single(result);
        }

        [Fact]
        public void DeleteGrupo()
        {
            var itemsInserted = new List<Grupo>();

            itemsInserted.Add(new Grupo()
            {
                GrupoId = 1
            });

            _repository.Setup(x => x.FindBy<Grupo>(1)).Returns(itemsInserted.Find(x => x.GrupoId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Grupo>())).Callback<Grupo>((entity) => itemsInserted.Remove(entity));

            // Act

            _service.DeleteGrupoById(1);

            // Assert

            Assert.Empty(itemsInserted);
        }

        [Fact]
        public void DeleteGrupo_NotFound()
        {
            var itemsInserted = new List<Grupo>();

            itemsInserted.Add(new Grupo()
            {
                GrupoId = 1
            });

            _repository.Setup(x => x.FindBy<Grupo>(1)).Returns(itemsInserted.Find(x => x.GrupoId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Grupo>())).Callback<Grupo>((entity) => itemsInserted.Remove(entity));

            // Act

            //_service.DeleteGrupoById(2);

            // Assert

            var ex = Assert.Throws<Exception>(() => _service.DeleteGrupoById(2));
        }
    }
}