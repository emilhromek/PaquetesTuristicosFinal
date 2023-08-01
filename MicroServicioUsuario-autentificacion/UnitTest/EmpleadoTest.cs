using Moq;
using System.Collections.Generic;
using System.Web.Http;
using Turismo.Template.Application.Services;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.Entities;
using Turismo.Template.Domain.Queries;
using Xunit;

namespace UnitTest
{
    public class EmpleadoTest
    {
        private readonly Mock<IRepositoryGeneric> _repository;
        private readonly Mock<IEmpleadoQuery> _query;
        private readonly EmpleadoServices _service;
        public EmpleadoTest()
        {
            _repository = new Mock<IRepositoryGeneric>();
            _query = new Mock<IEmpleadoQuery>();
            _service = new EmpleadoServices(_repository.Object, _query.Object);
        }

        [Fact]
        public void CreateEmpleado()
        {
            // Arrange

            var itemsInserted = new List<Empleado>();

            _repository.Setup(i => i.Add(It.IsAny<Empleado>()))
            .Callback((Empleado item) => itemsInserted.Add(item));

            // Act

            var dto = new EmpleadoDto()
            {
                Dni = 123456789,
            };

            _service.Create(dto);

            // Assert

            Assert.Single(itemsInserted);
        }

        [Fact]
        public void ActualizarEmpleado()
        {
            // Arrange

            var empleado = new Empleado()
            {
                EmpleadoId = 1
            };

            var dto = new EmpleadoDto()
            {
                Dni = 1
            };

            _repository.Setup(x => x.FindBy<Empleado>(1)).Returns(empleado);

            // Act

            _service.Update(1, dto);

            // Assert

            _repository.Verify(d => d.Update(It.IsAny<Empleado>()), Times.Once());

        }

        [Fact]
        public void ActualizarEmpleado_NotFound()
        {
            // Arrange

            var empleado = new Empleado()
            {
                EmpleadoId = 1
            };

            var dto = new EmpleadoDto()
            {
                Dni = 1
            };

            _repository.Setup(x => x.FindBy<Empleado>(1)).Returns(empleado);

            // Act

            //_service.Update(1, dto);

            // Assert

            var ex = Assert.Throws<HttpResponseException>(() => _service.Update(2, dto));

        }

        [Fact]
        public void DeleteEmpleado()
        {
            // Arrange

            var itemsInserted = new List<Empleado>();

            itemsInserted.Add(new Empleado()
            {
                EmpleadoId = 1
            });

            _repository.Setup(x => x.FindBy<Empleado>(1)).Returns(itemsInserted.Find(x => x.EmpleadoId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Empleado>())).Callback<Empleado>((entity) => itemsInserted.Remove(entity));

            // Act

            _service.deleteId(1);

            // Assert

            Assert.Empty(itemsInserted);
        }

        [Fact]
        public void DeleteEmpleado_NotFound()
        {
            // Arrange

            var itemsInserted = new List<Empleado>();

            itemsInserted.Add(new Empleado()
            {
                EmpleadoId = 1
            });

            _repository.Setup(x => x.FindBy<Empleado>(1)).Returns(itemsInserted.Find(x => x.EmpleadoId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<Empleado>())).Callback<Empleado>((entity) => itemsInserted.Remove(entity));

            // Act

            //_service.deleteId(1);

            // Assert

            var ex = Assert.Throws<HttpResponseException>(() => _service.deleteId(2));
        }

        [Fact]
        public void GetById()
        {
            // Arrange

            var empleado = new Empleado()
            {
                EmpleadoId = 1,
                UserId = 1,
            };

            var response = new EmpleadoDtoById()
            {
                EmpleadoId = 1,
                UserId = 1,
            };

            // Act

            _repository.Setup(x => x.FindBy<Empleado>(1)).Returns(empleado);

            var result = _service.getId(1);

            // Assert

            Assert.Equal(result.EmpleadoId, response.EmpleadoId);
        }

        [Fact]
        public void GetById_NotFound()
        {
            // Arrange

            var empleado = new Empleado()
            {
                EmpleadoId = 1,
                UserId = 1,
            };

            var response = new EmpleadoDtoById()
            {
                EmpleadoId = 1,
                UserId = 1,
            };

            // Act

            _repository.Setup(x => x.FindBy<Empleado>(1)).Returns(empleado);

            //var result = _service.getId(2);

            // Assert

            var ex = Assert.Throws<HttpResponseException>(() => _service.getId(2));
        }

        [Fact]
        public void GetAll()
        {
            // Arrange

            var empleadoList = new List<Empleado>()
            {
                new Empleado()
                {
                    EmpleadoId = 1,
                }
            };

            _repository.Setup(x => x.Traer<Empleado>()).Returns(empleadoList);

            // Act

            var result = _service.getAll();

            // Assert

            Assert.Single(result);
        }
    }
}