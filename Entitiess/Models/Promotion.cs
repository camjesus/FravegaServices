using System;
using System.Collections.Generic;

namespace FravegaService.Models
{
    public class Promotion
    {
        public Promotion(Guid id, IEnumerable<string> mediosDePago, IEnumerable<string> bancos, IEnumerable<string> categoriasProductos, int? maximaCantidadDeCuotas,
           decimal? valorInteresesCuotas, decimal? porcentajeDedescuento, DateTime? fechaInicio, DateTime? fechaFin, bool activo, DateTime fechaCreacion, DateTime? fechaModificacion)
        {
            Id = id;
            MediosDePago = mediosDePago;
            Bancos = bancos;
            CategoriasProductos = categoriasProductos;
            MaximaCantidadDeCuotas = maximaCantidadDeCuotas;
            ValorInteresesCuotas = valorInteresesCuotas;
            PorcentajeDedescuento = porcentajeDedescuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Activo = activo;
            FechaCreacion = fechaCreacion;
            FechaModificacion = fechaCreacion;
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
