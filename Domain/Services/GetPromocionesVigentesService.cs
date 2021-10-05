using Domain.Core.Data;
using Entities = FravegaService.Models;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FravegaService.Services
{
    public interface IGetPromocionesVigentesService
    {
        Task<IEnumerable<CurrentPromotion>> GetPromocionesVigentes(DateTime fecha, string banco, string medioDePago, IEnumerable<string> categorias);
    }

    public class GetPromocionesVigentesService : IGetPromocionesVigentesService
    {
        private readonly IPromotionRepository _promotion;

        public GetPromocionesVigentesService(IPromotionRepository promotion)
        {
            _promotion = promotion;
        }

        public async Task<IEnumerable<CurrentPromotion>> GetPromocionesVigentes(DateTime fechaFin, string banco, string medioDePago, IEnumerable<string> categorias)
        {
            List<CurrentPromotion> promoCurrent = new List<CurrentPromotion>();

            var promosEntity = await _promotion.FindByActivoAndFechaInicioGreaterThanEqualAndFechaFinLeesThanEqual(true, DateTime.Now, fechaFin);


            foreach (Entities.Promotion p in promosEntity)
            {
                CurrentPromotion cp = new CurrentPromotion();
                cp.Id = p.Id;
                cp.MaximaCantidadDeCuotas = p.MaximaCantidadDeCuotas;
                cp.MediosDePago = p.MediosDePago;
                cp.Bancos = p.Bancos;
                cp.CategoriasProductos = p.CategoriasProductos;
                cp.PorcentajeDedescuento = p.PorcentajeDedescuento;
                cp.ValorInteresesCuotas = p.ValorInteresesCuotas;
                promoCurrent.Add(cp);
            }

            return promoCurrent;
        }
    }
}