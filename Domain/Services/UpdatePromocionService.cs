using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FravegaService.Models;
using FravegaService.Services.DTO;
using Microsoft.Extensions.Logging;
using FravegaService.Infrastructure;
using MongoDB.Driver;

namespace FravegaService.Services
{
    public interface IUpdatePromocionService
    {
        Task<Guid> UpdatePromocion(Promocion promo);
    }
    public class UpdatePromocionService : IUpdatePromocionService
    {
        private readonly ILogger<PromocionEntity> _logger;
        private readonly AppDbContext _db;
        private readonly ValidarPromocionService _validarPromocion;
        private readonly IMapper _mappper;
        public UpdatePromocionService(ILogger<PromocionEntity> logger, AppDbContext db, IMapper mapper, ValidarPromocionService validarPromocion)
        {
            _logger = logger;
            _db = db;
            _mappper = mapper;
            _validarPromocion = validarPromocion;
        }

        public async Task<Guid> UpdatePromocion(Promocion promo)
        {
            _validarPromocion.ValidarPromocion(promo);

            var filter = Builders<PromocionEntity>.Filter.Eq(s => s.Id, promo.Id);
            var update = Builders<PromocionEntity>.Update.Set(s => s.MediosDePago, promo.MediosDePago).Set(s => s.Bancos, promo.Bancos)
                                                         .Set(s => s.CategoriasProductos, promo.CategoriasProductos).Set(s => s.MaximaCantidadDeCuotas, promo.MaximaCantidadDeCuotas)
                                                         .Set(s => s.ValorInteresesCuotas, promo.ValorInteresesCuotas).Set(s => s.PorcentajeDedescuento, promo.PorcentajeDedescuento)
                                                         .Set(s => s.FechaInicio, promo.FechaInicio).Set(s => s.FechaFin, promo.FechaFin).Set(s => s.FechaModificacion, DateTime.Now);

            await _db.Promociones.UpdateOneAsync(filter, update);
            return promo.Id;
        }
    }
}
