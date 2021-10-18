using Domain.Core.Data;
using FravegaService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Domain.Core.Services
{
    public interface IGetPromocionByIdService
    {
        Task<Promotion> GetPromocionById(Guid id);
    }

    public class GetPromocionByIdService : IGetPromocionByIdService
    {
        private readonly IPromotionRepository _promotion;

        public GetPromocionByIdService(IPromotionRepository promotion)
        {
            _promotion = promotion ?? throw new ArgumentNullException(nameof(promotion));
        }

        public async Task<Promotion> GetPromocionById(Guid id)
        {
            //_logger.LogInformation("Se consulta promocion por Id" + id);
            return await _promotion.FindOneAsync(id);
        }
    }
}