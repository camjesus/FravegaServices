using AutoFixture.Xunit2;
using Domain.Core.Data;
using Entities = FravegaService.Models;
using Domain.Core.Exceptions;
using Domain.Core.Services;
using Domain.Core.Tests.Services.Customizations;
using FluentAssertions;
using FravegaService.Domain.Core.DTO;
using Moq;
using System;
using System.Threading.Tasks;
using Tests.Utils;
using Xunit;

namespace Domain.Core.Tests.Services
{
    public class UpdateVigenciaServiceTests
    {
        [Theory]
        [DefaultData]
        public async Task UpdateVigencia_Valid_ShouldChangeVigencia(
            Promotion dto,
            Guid id,
            [Frozen] Mock<IValidarPromocionService> validarPromocionServiceMock,
            [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
            UpdateVigenciaService sut
            )
        {
            //arrange
            DateTime fechaInicio = new DateTime(2021, 2, 10);
            DateTime fechaFin = new DateTime(2021, 2, 20);

            validarPromocionServiceMock
               .Setup(x => x.ValidarAsync(dto))
               .Verifiable();

            Entities.Promotion entidadModificada = null;

            promotionRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Entities.Promotion>()))
                .Callback((Entities.Promotion x) => entidadModificada = x);

            //act

            var result = await sut.UpdateVigencia(id, fechaInicio, fechaFin);

            //assert

            validarPromocionServiceMock
                .Verify(x => x.ValidarAsync(dto), Times.Once);

            promotionRepositoryMock
                .Verify(x => x.UpdateAsync(entidadModificada), Times.Once);

            entidadModificada.FechaInicio.Should().Be(fechaInicio);
            entidadModificada.FechaFin.Should().Be(fechaFin);

            result.Should().Be(entidadModificada.Id);
        }

        [Theory]
        [DefaultData]
        public async Task UpdateVigencia_Valid_ShouldThrow(
            Promotion dto,
            Guid id,
            [Frozen] Mock<IValidarPromocionService> validaPromocionServiceMock,
            [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
            Exception exception,
            UpdateVigenciaService sut
            )
        {
            //arrange
            DateTime fechaInicio = new DateTime(2021, 2, 10);
            DateTime fechaFin = new DateTime(2021, 2, 20);

            validaPromocionServiceMock
                .Setup(x => x.ValidarAsync(dto))
                .Throws(exception);

            promotionRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Entities.Promotion>()))
                .Verifiable();

            //act

            Func<Task> func = async () => await sut.UpdateVigencia(id, fechaInicio, fechaFin);

            //assert

            await func
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage(exception.Message);

            validaPromocionServiceMock
                .Verify(x => x.ValidarAsync(dto), Times.Once);

            promotionRepositoryMock
                .Verify(x => x.UpdateAsync(It.IsAny<Entities.Promotion>()), Times.Never);
        }
    }
}
