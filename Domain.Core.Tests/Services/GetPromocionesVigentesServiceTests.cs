using AutoFixture.Xunit2;
using Domain.Core.Data;
using Domain.Core.Services;
using FluentAssertions;
using FravegaService.Domain.Core.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Utils;
using Xunit;
using Entities = FravegaService.Models;

namespace Domain.Core.Tests.Services
{
    public class GetPromocionesVigentesServiceTests
    {
        [Theory]
        [DefaultData]
        public async Task GetPromocionesVigentes_Valid_ShouldNotThrowException(
            Promotion dto,
            IEnumerable<Entities.Promotion> promotions,
            [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
            GetPromocionesVigentesService sut 
            )
        {
            //arrange
            DateTime fechaFin = new DateTime(2021, 12, 20);

            promotionRepositoryMock
                .Setup(x => x.FindByActivoAndFechaInicioGreaterThanEqualAndFechaFinLeesThanEqual( DateTime.Now.Date, fechaFin))
                .ReturnsAsync(promotions);

            //act
            Func<Task> func = async () => await sut.GetPromocionesVigentes(fechaFin, dto.Bancos, dto.MediosDePago, dto.CategoriasProductos);

            //assert
            await func
                 .Should()
                 .NotThrowAsync();

            promotionRepositoryMock
                .Verify(x => x.FindByActivoAndFechaInicioGreaterThanEqualAndFechaFinLeesThanEqual(DateTime.Now.Date, fechaFin), Times.Once);
        }


        [Theory]
        [DefaultData]
        public async Task GetPromocionById_Valid_ShouldThrowException(
             Promotion dto,
             Exception exception,
             [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
             GetPromocionesVigentesService sut
             )
        {
            //arrange
            DateTime fechaFin = new DateTime(2021, 12, 20);

            promotionRepositoryMock
                .Setup(x => x.FindByActivoAndFechaInicioGreaterThanEqualAndFechaFinLeesThanEqual(DateTime.Now.Date, fechaFin))
                .Throws(exception);

            //act
            Func<Task> func = async () => await sut.GetPromocionesVigentes(fechaFin, dto.Bancos, dto.MediosDePago, dto.CategoriasProductos);

            //assert
            await func
                 .Should()
                 .ThrowAsync<Exception>();

            promotionRepositoryMock
                .Verify(x => x.FindByActivoAndFechaInicioGreaterThanEqualAndFechaFinLeesThanEqual(DateTime.Now.Date, fechaFin), Times.Once);
        }
    }

}
 