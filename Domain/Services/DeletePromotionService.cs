using Entities = FravegaService.Models;
using Domain.Core.Data;
using Domain.Core.Services;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Domain.Core.Exceptions;

namespace Domain.Core.Services
{
    public interface IDeletePromotionService
    {
        Task<Guid> DeletePromotion(Guid id);
    }

    public class DeletePromotionService : IDeletePromotionService
    {
        private readonly IPromotionRepository _promotion;

        public DeletePromotionService( IPromotionRepository promotion)
        {
            _promotion = promotion ?? throw new ArgumentNullException(nameof(promotion));
        }

        public async Task<Guid> DeletePromotion(Guid id)
        {
            var promotionEntity = await _promotion.FindOneAsync(id);

            try
            {
                promotionEntity.Delete();
            }
            catch (ArgumentException)
            {
                throw new PromotionAlreadyDeletedException();
            }

            await _promotion.UpdateAsync(promotionEntity);

            return id;
        }
    }
}