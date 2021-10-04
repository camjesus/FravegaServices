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
        [InlineDefaultData(1, null, null)]
        [InlineDefaultData(1, 1, null)]
        [InlineDefaultData(null, null, 1)]
        public void ValidarCuotas_Valid_ShouldNotThrowException(
            int? maximaCantidadCuotas,
            int? valorInteresesCuotas,
            int? porcentajeDescuento,
            Promotion promotion,
            ValidarCuotasService sut)
        {
            //arrange

            promotion.MaximaCantidadDeCuotas = maximaCantidadCuotas;
            promotion.ValorInteresesCuotas = valorInteresesCuotas;
            promotion.PorcentajeDedescuento = porcentajeDescuento;

            //act

            Action act = () => sut.ValidarCuotas(promotion);

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

            Action act = () => sut.ValidarCuotas(promotion);

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

            Action act = () => sut.ValidarCuotas(promotion);

            //assert

            act.Should().Throw<CantidadDeCuotasYPorcentajeAmbosNoPuedenTenerValorException>();
        }
    }
}