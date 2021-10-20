﻿using Domain.Core.Data;
using Entities = FravegaService.Models;
using FravegaService.Domain.Core.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Core.Services
{
    public interface IGetPromocionesVigentesService
    {
        Task<IEnumerable<CurrentPromotion>> GetPromocionesFilterVigentesAsync(IEnumerable<string> banco , IEnumerable<string> medioDePago, IEnumerable<string> categorias);
        Task<IEnumerable<CurrentPromotion>> GetPromocionesVigentesAsync(DateTime date);

    }

    public class GetPromocionesVigentesService : IGetPromocionesVigentesService
    {
        private readonly IPromotionRepository _promotion;

        public GetPromocionesVigentesService(IPromotionRepository promotion)
        {
            _promotion = promotion ?? throw new ArgumentNullException(nameof(promotion));
        }

        public async Task<IEnumerable<CurrentPromotion>> GetPromocionesFilterVigentesAsync(IEnumerable<string> bancos, IEnumerable<string> mediosDePago, IEnumerable<string> categorias)
        {
            List<CurrentPromotion> promoCurrent = new List<CurrentPromotion>();

            var promosEntity = await _promotion.FindPromocionesVigentesFilterAsync(categorias, mediosDePago, bancos);
            

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

        public async Task<IEnumerable<CurrentPromotion>> GetPromocionesVigentesAsync(DateTime date)
        {
            List<CurrentPromotion> promoCurrent = new List<CurrentPromotion>();

            var promosEntity = await _promotion.FindPromocionesVigentesAsync(date);


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