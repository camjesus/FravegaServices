using Domain.Core.Exceptions;
using FluentAssertions;
using FravegaService.Domain.Core.DTO;
using FravegaService.Services;
using System;
using Tests.Utils;
using Xunit;

namespace Domain.Core.Tests.Services
{
    public class ValidarCuotasServiceTests
    {
        //inline
        [Theory]
        [InlineDefaultData(1, null)]
        [InlineDefaultData(null, 1)]
        public void ValidarCuotas_Valid_ShouldNotThrowException(
            int? maximaCantidadCuotas,
            int? porcentajeDescuento,
            Promotion promotion,
            ValidarCuotasService sut)
        {
            //arrange

            promotion.MaximaCantidadDeCuotas = maximaCantidadCuotas;
            promotion.PorcentajeDedescuento = porcentajeDescuento;

            //act

            Action act = () => sut.ValidarCuotasYPorcentaje(promotion);

            //assert

            act.Should().NotThrow();
        }

        [Theory]
        [InlineDefaultData(null, null)]
        [InlineDefaultData(1, null)]
        public void ValidarCuotas_ValidValorInteres_ShouldNotThrowException(
           int? maximaCantidadCuotas,
           int? valorInteresesCuotas,
           Promotion promotion,
           ValidarCuotasService sut)
        {
            //arrange

            promotion.MaximaCantidadDeCuotas = maximaCantidadCuotas;
            promotion.ValorInteresesCuotas = valorInteresesCuotas;

            //act

            Action act = () => sut.ValidarValorInteres(promotion);

            //assert

            act.Should().NotThrow();
        }

        [Theory]
        [DefaultData]
        public void ValidarCuotas_CantidadCuotasYPorcentajeDescuentoNull_ShouldThrowCantidadDeCuotasOProcentajeDescuentoTieneQueTenerValorException(
            Promotion promotion,
            ValidarCuotasService sut)
        {
            //arrange

            promotion.MaximaCantidadDeCuotas = null;
            promotion.PorcentajeDedescuento = null;

            //act

            Action act = () => sut.ValidarCuotasYPorcentaje(promotion);

            //assert

            act.Should().Throw<CantidadDeCuotasOProcentajeDescuentoTieneQueTenerValorException>();
        }

        [Theory]
        [DefaultData]
        public void ValidarCuotas_CantidadCuotasYPorcentajeDescuentoNotNull_ShouldThrowCantidadDeCuotasYPorcentajeAmbosNoPuedenTenerValorException(
            Promotion promotion,
            ValidarCuotasService sut)
        {
            //arrange

            promotion.MaximaCantidadDeCuotas = 1;
            promotion.PorcentajeDedescuento = 1;

            //act

            Action act = () => sut.ValidarCuotasYPorcentaje(promotion);

            //assert

            act.Should().Throw<CantidadDeCuotasYPorcentajeAmbosNoPuedenTenerValorException>();
        }

        [Theory]
        [DefaultData]
        public void ValidarCuotas_ValorInteresNotNull_ShouldThrowAlAgregarValorDeIntesDebeTenerCantidadDeCuotasException(
           Promotion promotion,
           ValidarCuotasService sut)
        {
            //arrange

            promotion.MaximaCantidadDeCuotas = null;
            promotion.ValorInteresesCuotas = 1;

            //act

            Action act = () => sut.ValidarValorInteres(promotion);

            //assert

            act.Should().Throw<AlAgregarValorDeIntesDebeTenerCantidadDeCuotasException>();
        }
    }
}