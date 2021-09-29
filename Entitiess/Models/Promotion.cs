using System;
using System.Collections.Generic;

namespace FravegaService.Models
{
    public class Promotion
    {
        public Promotion(IEnumerable<string> mediosDePago, IEnumerable<string> bancos, IEnumerable<string> categoriasProductos, int? maximaCantidadDeCuotas,
           decimal? valorInteresesCuotas, decimal? porcentajeDedescuento, DateTime? fechaInicio, DateTime? fechaFin)
        {
            Id = Guid.NewGuid();
            MediosDePago = mediosDePago;
            Bancos = bancos;
            CategoriasProductos = categoriasProductos;
            MaximaCantidadDeCuotas = maximaCantidadDeCuotas;
            ValorInteresesCuotas = valorInteresesCuotas;
            PorcentajeDedescuento = porcentajeDedescuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Activo = true;
            FechaCreacion = DateTime.Now;
            FechaModificacion = null;
        }

        public Guid Id { get; private set; }
        public IEnumerable<string> MediosDePago { get; private set; }
        public IEnumerable<string> Bancos { get; private set; }
        public IEnumerable<string> CategoriasProductos { get; private set; }
        public int? MaximaCantidadDeCuotas { get; private set; }
        public decimal? ValorInteresesCuotas { get; private set; }
        public decimal? PorcentajeDedescuento { get; private set; }
        public DateTime? FechaInicio { get; private set; }
        public DateTime? FechaFin { get; private set; }

        public bool Activo { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaModificacion { get; private set; }
    }
}
