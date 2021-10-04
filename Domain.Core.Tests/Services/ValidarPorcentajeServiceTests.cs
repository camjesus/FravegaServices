using FluentAssertions;
using FravegaService.Domain.Core.DTO;
using FravegaService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Utils;
using Xunit;

namespace Domain.Core.Tests.Services
{
    public class ValidarPorcentajeServiceTests
    {
        [Theory]
        [DefaultData]
        public void ValidarPorcentaje_Valid_ShouldNotThrowException(
            Promotion promotion,
            ValidarPorcentajeService sut)
        {
            //arrange

            promotion.PorcentajeDedescuento = 5;

            //act

            Action act = () => sut.ValidarPorcentaje(promotion); 

            //assert

            act.Should().NotThrow();
        }

        [Theory]
        [InlineDefaultData(2, null)]
        [InlineDefaultData(5, null, null)]
        [InlineDefaultData(100, null, null)]
        public void ValidarPorcentaje_Valid2_ShouldNotThrowException(int input,
           Promotion promotion,
           ValidarPorcentajeService sut)
        {
            //arrange
           
            promotion.PorcentajeDedescuento = input;

            //act

            Action act = () => sut.ValidarPorcentaje(promotion);

            //assert
            
            act.Should().NotThrow();
        }
    }

}
