using Moq;
using System.Collections.Generic;
using System.Web.Http;
using Turismo.Template.Application.Services;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.Entities;
using Xunit;

namespace UnitTest
{
    public class MetodoPagoTest
    {
        private readonly Mock<IRepositoryGeneric> _repository;
        private readonly MetodoPagoServices _service;
        public MetodoPagoTest()
        {
            _repository = new Mock<IRepositoryGeneric>();
            _service = new MetodoPagoServices(_repository.Object);
        }

        [Fact]
        public void CreateMetodoPago()
        {
            // Arrange

            var itemsInserted = new List<MetodoPago>();

            _repository.Setup(i => i.Add(It.IsAny<MetodoPago>()))
            .Callback((MetodoPago item) => itemsInserted.Add(item));

            // Act

            var dto = new MetodoPagoDto()
            {
                Nombre = "Metodo",
            };

            _service.Create(dto);

            // Assert

            Assert.Single(itemsInserted);
        }


        [Fact]
        public void DeleteMetodoPago()
        {
            // Arrange

            var itemsInserted = new List<MetodoPago>();

            itemsInserted.Add(new MetodoPago()
            {
                MetodoPagoId = 1
            });

            _repository.Setup(x => x.FindBy<MetodoPago>(1)).Returns(itemsInserted.Find(x => x.MetodoPagoId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<MetodoPago>())).Callback<MetodoPago>((entity) => itemsInserted.Remove(entity));

            // Act

            _service.deleteId(1);

            // Assert

            Assert.Empty(itemsInserted);
        }

        [Fact]
        public void DeleteMetodoPago_NotFound()
        {
            // Arrange

            var itemsInserted = new List<MetodoPago>();

            itemsInserted.Add(new MetodoPago()
            {
                MetodoPagoId = 1
            });

            _repository.Setup(x => x.FindBy<MetodoPago>(1)).Returns(itemsInserted.Find(x => x.MetodoPagoId == 1));

            _repository.Setup(m => m.Delete(It.IsAny<MetodoPago>())).Callback<MetodoPago>((entity) => itemsInserted.Remove(entity));

            // Act

            //_service.deleteId(1);

            // Assert

            var ex = Assert.Throws<HttpResponseException>(() => _service.deleteId(2));
        }

        [Fact]
        public void GetById()
        {
            // Arrange

            var metodoPago = new MetodoPago()
            {
                MetodoPagoId = 1,
            };

            var response = new MetodoPagoDtoById()
            {
                MetodoPagoId = 1,
            };

            // Act

            _repository.Setup(x => x.FindBy<MetodoPago>(1)).Returns(metodoPago);

            var result = _service.getId(1);

            // Assert

            Assert.Equal(result.MetodoPagoId, response.MetodoPagoId);
        }

        [Fact]
        public void GetById_NotFound()
        {
            // Arrange

            var metodoPago = new MetodoPago()
            {
                MetodoPagoId = 1,
            };

            var response = new MetodoPagoDtoById()
            {
                MetodoPagoId = 1,
            };

            _repository.Setup(x => x.FindBy<MetodoPago>(1)).Returns(metodoPago);

            // Act

            // var result = _service.getId(1);

            // Assert

            var ex = Assert.Throws<HttpResponseException>(() => _service.getId(2));
        }

        [Fact]
        public void GetAll()
        {
            // Arrange

            var metodoPagoList = new List<MetodoPago>()
            {
                new MetodoPago()
                {
                    MetodoPagoId = 1,
                }
            };

            _repository.Setup(x => x.Traer<MetodoPago>()).Returns(metodoPagoList);

            // Act

            var result = _service.getAll();

            // Assert

            Assert.Single(result);
        }
    }
}