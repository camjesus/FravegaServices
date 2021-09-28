using FluentAssertions;
using FravegaService.Services;
using FravegaService.Services.DTO;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Core.Tests
{
    public class PromocionTest
    {
        [Theory]
        [MemberData(nameof(GetPromocionByIdService))]
        public async Task ObtenerPromocionAsync(GetPromocionByIdService serv)
        {
            //Preparación
            Guid id = new Guid("casdsadsa");
            var mock = new Mock<Promocion>();
            mock.SetupAllProperties();
            mock.SetupGet(p => p.Id).Returns(id);

            //Ejecución
            Func<Task> act = async () => await serv.GetPromocionById(id);

            //Evaluación
            //Assert.NotNull(act);
            await act.Should().NotThrowAsync();
           
        }
    }
}