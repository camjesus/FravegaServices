using Domain.Core.Exceptions;
using FluentAssertions;
using FravegaService.Domain.Core.DTO;
using Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Utils;
using Xunit;

namespace Domain.Core.Tests.Services
{
    public class ValidarExistenciaServiceTests
    {
        [Theory]
        [DefaultData]
        public void ValidarExistencia_Valid_ShouldNotThrowException(
            Promotion dto,
            ValidarExistenciaService sut)
        {
            //arrange

            DateTime fechaInicio = new DateTime(2021,01,1);
            DateTime fechaFin = new DateTime(2021, 01, 10);

            //act

            Action act = () => sut.ValidarExistencia(dto); 

            //assert

            act.Should().NotThrow();
        }

        [Theory]
        [DefaultData]
        public void ValidarFechas_Valid_ShouldThrowFechaInicioMayorFechaFinException(
                ValidarFechasService sut)
        {
            //arrange

            DateTime fechaInicio = new DateTime(2021, 01, 10);
            DateTime fechaFin = new DateTime(2021, 01, 1);

            //act

            Action act = () => sut.ValidarFechas(fechaInicio, fechaFin);

            //assert

            act.Should().Throw<FechaInicioMayorFechaFinException>();
        }
    }

}
