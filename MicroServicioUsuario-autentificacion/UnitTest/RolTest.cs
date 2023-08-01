using Moq;
using System.Collections.Generic;
using Turismo.Template.Application.Services;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.DTO;
using Turismo.Template.Domain.Entities;
using Xunit;

namespace UnitTest
{
    public class RolTest
    {
        private readonly Mock<IRepositoryGeneric> _repository;
        private readonly RollServices _service;
        public RolTest()
        {
            _repository = new Mock<IRepositoryGeneric>();
            _service = new RollServices(_repository.Object);
        }

        [Fact]
        public void GetById()
        {
            // Arrange

            var rol = new Roll()
            {
                RollId = 1,
            };

            var response = new RollDto()
            {
                RollId = 1,
            };

            // Act

            _repository.Setup(x => x.FindBy<Roll>(1)).Returns(rol);

            var result = _service.getId(1);

            // Assert

            Assert.Equal(result.RollId, response.RollId);
        }

        [Fact]
        public void GetAll()
        {
            // Arrange

            var rolList = new List<Roll>()
            {
                new Roll()
                {
                    RollId = 1,
                }
            };

            _repository.Setup(x => x.Traer<Roll>()).Returns(rolList);

            // Act

            var result = _service.getAll();

            // Assert

            Assert.Single(result);
        }
    }
}