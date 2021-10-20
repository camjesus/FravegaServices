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
    public class UpdatePromocionServiceTests
    {
        [Theory]
        [DefaultData]
        public async Task UpdatePromocion_Valid_ShouldChangePromotion(
            PromotionUpd dto,
            Entities.Promotion promotion,
            [Frozen] Mock<IValidarPromocionService> validarPromocionServiceMock,
            [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
            UpdatePromocionService sut
            )
        {
            validarPromocionServiceMock
               .Setup(x => x.ValidarAsync(dto))
               .Verifiable();

            Entities.Promotion entidadModificada = null;

            promotionRepositoryMock
               .Setup(x => x.FindOneAsync(dto.Id))
               .ReturnsAsync(promotion);

            promotionRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Entities.Promotion>()))
                .Callback((Entities.Promotion x) => entidadModificada = x);

            //act

            var result = await sut.UpdatePromocion(dto);

            //assert
            
            validarPromocionServiceMock
                .Verify(x => x.ValidarAsync(dto), Times.Once);

            promotionRepositoryMock
                .Verify(x => x.UpdateAsync(entidadModificada), Times.Once);

            promotionRepositoryMock
               .Verify(x => x.FindOneAsync(dto.Id), Times.Once);

            entidadModificada.MediosDePago.Should().BeEquivalentTo(dto.MediosDePago);
            entidadModificada.Bancos.Should().BeEquivalentTo(dto.Bancos);
            entidadModificada.CategoriasProductos.Should().BeEquivalentTo(dto.CategoriasProductos);
            entidadModificada.MaximaCantidadDeCuotas.Should().Be(dto.MaximaCantidadDeCuotas);
            entidadModificada.ValorInteresesCuotas.Should().Be(dto.ValorInteresesCuotas);
            entidadModificada.PorcentajeDedescuento.Should().Be(dto.PorcentajeDedescuento);
            entidadModificada.FechaInicio.Should().Be(dto.FechaInicio);
            entidadModificada.FechaFin.Should().Be(dto.FechaFin);

            result.Should().Be(entidadModificada.Id);
        }

        [Theory]
        [DefaultData]
        public async Task UpdatePromocion_Valid_ShouldThrow(
           PromotionUpd dto,
            [Frozen] Mock<IValidarPromocionService> validaPromocionServiceMock,
            [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
            Exception exception,
            UpdatePromocionService sut
            )
        {
            //arrange
            Entities.Promotion entidadModificada = null;

            validaPromocionServiceMock
                .Setup(x => x.ValidarAsync(dto))
                .Throws(exception);

            promotionRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Entities.Promotion>()))
                .Verifiable();

            promotionRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Entities.Promotion>()))
                .Callback((Entities.Promotion x) => entidadModificada = x);
            //act

            Func<Task> func = async () => await sut.UpdatePromocion(dto);

            //assert

            await func
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage(exception.Message);

            validaPromocionServiceMock
                .Verify(x => x.ValidarAsync(dto), Times.Once);

            promotionRepositoryMock
                .Verify(x => x.AddAsync(It.IsAny<Entities.Promotion>()), Times.Never);
        }
    }
}
