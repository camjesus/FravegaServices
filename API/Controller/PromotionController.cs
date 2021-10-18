using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities = FravegaService.Models;
using Domain.Core.Services;
using FravegaService.Domain.Core.DTO;

namespace API.Controller
{
    [Route("promotion")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IAddPromocionEntityService _addPromocionEntity;
        public PromotionController(IAddPromocionEntityService addPromocionEntity)
        {
            this._addPromocionEntity = addPromocionEntity;
        }

        [HttpPost("add")]
        [Produces("application/json")]
        [ProducesResponseType(200, StatusCode = 200, Type = typeof(Guid))]
        public async Task<Guid> GetClientAccount(Promotion promotion)
        {
            return await _addPromocionEntity.AddPromocionEntity(promotion);
        }
    }
}
