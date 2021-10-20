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
            IEnumerable<Entities.Promotion> promotions,
            [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
            GetPromocionesVigentesService sut 
            )
        {
            //arrange
            DateTime fecha = new DateTime(2021, 12, 20);

            promotionRepositoryMock
                .Setup(x => x.FindPromocionesVigentesAsync(fecha))
                .ReturnsAsync(promotions);

            //act
            Func<Task> func = async () => await sut.GetPromocionesVigentesAsync(fecha);

            //assert
            await func
                 .Should()
                 .NotThrowAsync();

            promotionRepositoryMock
                .Verify(x => x.FindPromocionesVigentesAsync(fecha), Times.Once);
        }


        [Theory]
        [DefaultData]
        public async Task GetPromocionById_Valid_ShouldThrowException(
             Exception exception,
             [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
             GetPromocionesVigentesService sut
             )
        {
            //arrange
            DateTime fecha = new DateTime(2021, 12, 20);

            promotionRepositoryMock
                .Setup(x => x.FindPromocionesVigentesAsync(fecha))
                .Throws(exception);

            //act
            Func<Task> func = async () => await sut.GetPromocionesVigentesAsync(fecha);

            //assert
            await func
                 .Should()
                 .ThrowAsync<Exception>();

            promotionRepositoryMock
                .Verify(x => x.FindPromocionesVigentesAsync(fecha), Times.Once);
        }
    }

}
 