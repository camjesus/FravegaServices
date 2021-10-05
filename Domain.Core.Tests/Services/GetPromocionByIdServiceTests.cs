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
    public class GetPromocionByIdServiceTests
    {
        [Theory]
        [DefaultData]
        public async Task GetPromocionById_Valid_ShouldNotThrowException(
            Guid id,
            Entities.Promotion promotion,
            [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
            GetPromocionByIdService sut 
            )
        {
            //arrange
            promotionRepositoryMock
                .Setup(x => x.FindOneAsync(id))
                .ReturnsAsync(promotion);

            //act
            Func<Task> func = async () => await sut.GetPromocionById(id);

            //assert
            await func
                 .Should()
                 .NotThrowAsync();

            promotionRepositoryMock
                .Verify(x => x.FindOneAsync(id), Times.Once);
        }


        [Theory]
        [DefaultData]
        public async Task GetPromocionById_Valid_ShouldThrowException(
             Guid id,
             Exception exception,
             [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
             GetPromocionByIdService sut
             )
        {
            //arrange
            promotionRepositoryMock
                .Setup(x => x.FindOneAsync(id))
                .Throws(exception);

            //act
            Func<Task> func = async () => await sut.GetPromocionById(id);

            //assert
            await func
                 .Should()
                 .ThrowAsync<Exception>();

            promotionRepositoryMock
                .Verify(x => x.FindOneAsync(id), Times.Once);
        }
    }

}
 