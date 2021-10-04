using AutoFixture.Xunit2;
using Domain.Core.Data;
using Domain.Core.Services;
using FluentAssertions;
using FravegaService.Domain.Core.DTO;
using FravegaService.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Tests.Utils;
using Xunit;
using Entities = FravegaService.Models;

namespace Domain.Core.Tests.Services
{
    public class AddPromocionEntityServiceTests
    {

        [Theory]
        [DefaultData]
        public async Task AddPromocionEntity_Valid_ShouldWork(
            Promotion dto,
            [Frozen] Mock<IValidarPromocionService> validarCrearPromocionServiceMock,
            [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
            AddPromocionEntityService sut //system under test
            )
        {
            //arrange

            validarCrearPromocionServiceMock
                .Setup(x => x.ValidarAsync(dto))
                .Verifiable();

            Entities.Promotion entidadCreada = null;

            promotionRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Entities.Promotion>()))
                .Callback((Entities.Promotion x) => entidadCreada = x);

            //act

            var result = await sut.AddPromocionEntity(dto);

            //assert

            validarCrearPromocionServiceMock
                .Verify(x => x.ValidarAsync(dto), Times.Once);

            promotionRepositoryMock
                .Verify(x => x.AddAsync(entidadCreada), Times.Once);

            entidadCreada.MediosDePago.Should().BeEquivalentTo(dto.MediosDePago);
            entidadCreada.Bancos.Should().BeEquivalentTo(dto.Bancos);
            entidadCreada.CategoriasProductos.Should().BeEquivalentTo(dto.CategoriasProductos);
            entidadCreada.MaximaCantidadDeCuotas.Should().Be(dto.MaximaCantidadDeCuotas);
            entidadCreada.ValorInteresesCuotas.Should().Be(dto.ValorInteresesCuotas);
            entidadCreada.PorcentajeDedescuento.Should().Be(dto.PorcentajeDedescuento);
            entidadCreada.FechaInicio.Should().Be(dto.FechaInicio);
            entidadCreada.FechaFin.Should().Be(dto.FechaFin);

            result.Should().Be(entidadCreada.Id);
        }

        [Theory]
        [DefaultData]
        public async Task AddPromocionEntity_ValidationThrowException_ShouldThrowException(
            Promotion dto,
            [Frozen] Mock<IValidarPromocionService> validarCrearPromocionServiceMock,
            [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
            Exception exception,
            AddPromocionEntityService sut //system under test
            )
        {
            //arrange

            validarCrearPromocionServiceMock
                .Setup(x => x.ValidarAsync(dto))
                .Throws(exception);

            promotionRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Entities.Promotion>()))
                .Verifiable();

            //act

            Func<Task> func = async () => await sut.AddPromocionEntity(dto);

            //assert

            await func
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage(exception.Message);

            validarCrearPromocionServiceMock
                .Verify(x => x.ValidarAsync(dto), Times.Once);

            promotionRepositoryMock
                .Verify(x => x.AddAsync(It.IsAny<Entities.Promotion>()), Times.Never);
        }

    }
}