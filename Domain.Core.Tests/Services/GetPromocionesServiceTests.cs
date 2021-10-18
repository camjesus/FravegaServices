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
    public class GetPromocionesServiceTests
    {
        [Theory]
        [DefaultData]
        public async Task GetPromociones_Valid_ShouldNotThrowException(
            [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
            GetPromocionesService sut 
            )
        {
            //arrange
            promotionRepositoryMock
                .Setup(x => x.GetAllAsync())
                .Verifiable();

            //act
            Func<Task> func = async () => await sut.GetPromociones();

            //assert
            await func
                 .Should()
                 .NotThrowAsync();

            promotionRepositoryMock
                .Verify(x => x.GetAllAsync(), Times.Once);
        }


        [Theory]
        [DefaultData]
        public async Task GetPromocionById_Valid_ShouldThrowException(
             Exception exception,
             [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
             GetPromocionesService sut
             )
        {
            //arrange
            DateTime fechaFin = new DateTime(2021, 12, 20);

            promotionRepositoryMock
                .Setup(x => x.GetAllAsync())
                .Throws(exception);

            //act
            Func<Task> func = async () => await sut.GetPromociones();

            //assert
            await func
                 .Should()
                 .ThrowAsync<Exception>();

            promotionRepositoryMock
                .Verify(x => x.GetAllAsync(), Times.Once);
        }
    }

}
 