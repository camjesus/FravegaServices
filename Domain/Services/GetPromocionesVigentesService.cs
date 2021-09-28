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
    public interface IGetPromocionesVigentesService
    {
        Task<List<PromocionVigente>> GetPromocionesVigentes(DateTime fecha, string banco, string medioDePago, IEnumerable<string> categorias);
    }
    public class GetPromocionesVigentesService : IGetPromocionesVigentesService
    {
        private readonly ILogger<PromocionEntity> _logger;
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public GetPromocionesVigentesService(ILogger<PromocionEntity> logger, AppDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<PromocionVigente>> GetPromocionesVigentes(DateTime fecha, string banco, string medioDePago, IEnumerable<string> categorias)
        {
            List<PromocionVigente> promosVigentes = new List<PromocionVigente>();

            var filter = Builders<PromocionEntity>.Filter.Eq(x => x.Activo, true);

            var promosEntity = await _db.Promociones.Find(x => x.Activo == true && x.FechaFin <= DateTime.Now && x.FechaInicio >= fecha).ToListAsync();
            //BUSCAR SEARCH STRING IN ARRAY  
            //var @event = await _db.Promociones.Find($"{{ _id: ObjectId('507f1f77bcf86cd799439011') }}").SingleAsync();

            return _mapper.Map<List<PromocionVigente>>(promosEntity);
        }
    }
}
