using Entities = FravegaService.Models;
using Domain.Core.Data;
using Domain.Core.Services;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FravegaService.Services
{
    public interface IDeletePromotionService
    {
        Task<Guid> Delete(Guid id);
    }

    public class DeletePromotionService : IDeletePromotionService
    {
        private readonly ILogger<Promotion> _logger;
        private readonly IPromotionRepository _promotion;

        public DeletePromotionService(ILogger<Promotion> logger, IPromotionRepository promotion)
        {
            _logger = logger;
            _promotion = promotion;
        }

        public async Task<Guid> Delete(Guid id)
        {
            _logger.LogInformation("Borrado logico de Id:" + id);
            var promotionEntity = await _promotion.FindOneAsync(id);

            promotionEntity.Delete();

            await _promotion.UpdateAsync(promotionEntity);

            return id;
        }
    }
}