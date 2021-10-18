using AutoFixture.Xunit2;
using Domain.Core.Data;
using Domain.Core.Exceptions;
using Domain.Core.Services;
using Domain.Core.Tests.Services.Customizations;
using FluentAssertions;
using FravegaService.Models;
using Moq;
using System;
using System.Threading.Tasks;
using Tests.Utils;
using Xunit;
using Entities = FravegaService.Models;

namespace Domain.Core.Tests.Services
{
    public class DeletePromotionServiceTests
    {
        [Theory]
        [DefaultData(typeof(ActivePromotionCustomization))]
        public async Task DeletePromotion_Valid_ShouldChangeActivoFalse(
            Guid id,
            Promotion promotion,
            [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
            DeletePromotionService sut
            )
        {
            //arrange

            promotionRepositoryMock
                  .Setup(x => x.FindOneAsync(id))
                  .ReturnsAsync(promotion);

            promotionRepositoryMock
                 .Setup(x => x.UpdateAsync(promotion))
                 .Verifiable();

            //act
            var result = await sut.DeletePromotion(id);

            //assert
            result.Should().Be(id);

            promotionRepositoryMock
                .Verify(x => x.FindOneAsync(id), Times.Once);

            promotionRepositoryMock
                 .Verify(x => x.UpdateAsync(promotion), Times.Once);

            promotion.Activo.Should().BeFalse();
        }

        [Theory]
        [DefaultData(typeof(InactivePromotionCustomization))]
        public async Task DeletePromotion_PromotionAlreadyDeleted_ShouldChangeActivoFalse(
            Guid id,
            Promotion promotion,
            [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
            DeletePromotionService sut
            )
        {
            //arrange

            promotionRepositoryMock
                  .Setup(x => x.FindOneAsync(id))
                  .ReturnsAsync(promotion);

            promotionRepositoryMock
                 .Setup(x => x.UpdateAsync(promotion))
                 .Verifiable();

            //act
            Func<Task> func = async () => await sut.DeletePromotion(id);

            //assert
            await func.Should().ThrowAsync<PromotionAlreadyDeletedException>();

            promotionRepositoryMock
                .Verify(x => x.FindOneAsync(id), Times.Once);

            promotionRepositoryMock
                 .Verify(x => x.UpdateAsync(promotion), Times.Never);
        }

        [Theory]
        [DefaultData]
        public async Task DeletePromotion_ValidationThrowException_ShouldThrowException(
             Guid id,
             Promotion promotion,
             Exception exception,
             [Frozen] Mock<IPromotionRepository> promotionRepositoryMock,
             DeletePromotionService sut
            )
        {
            //arrange

            promotionRepositoryMock
                 .Setup(x => x.FindOneAsync(id))
                 .Throws(exception);

            promotionRepositoryMock
                    .Setup(x => x.UpdateAsync(promotion))
                    .Verifiable();

            //act
            Func<Task> func = async () => await sut.DeletePromotion(id);

            //assert

            await func
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage(exception.Message);

            promotionRepositoryMock
                .Verify(x => x.FindOneAsync(id), Times.Once);

            promotionRepositoryMock
                .Verify(x => x.UpdateAsync(It.IsAny<Entities.Promotion>()), Times.Never);
        }
    }
}
