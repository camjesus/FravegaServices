using Domain.Core.Data;
using FravegaService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Core.Services
{
    public interface IGetPromocionesService
    {
        Task<IEnumerable<Promotion>> GetPromociones();
    }

    public class GetPromocionesService : IGetPromocionesService
    {
        private readonly IPromotionRepository _promotion;

        public GetPromocionesService(IPromotionRepository promotion)
        {
            _promotion = promotion ?? throw new ArgumentNullException(nameof(promotion));
        }

        public async Task<IEnumerable<Promotion>> GetPromociones()
        {
            //_logger.LogInformation("Obtengo todas las promociones");
            return await _promotion.GetAllAsync();
        }
    }
}