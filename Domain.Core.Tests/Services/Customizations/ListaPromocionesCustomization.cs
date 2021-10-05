using AutoFixture;
using FravegaService.Models;
using System;
using System.Collections.Generic;

namespace Domain.Core.Tests.Services.Customizations
{
    public class ListaPromocionesCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            
              var promotion = new Promotion(
                fixture.Create<IEnumerable<string>>(),
                fixture.Create<IEnumerable<string>>(),
                fixture.Create<IEnumerable<string>>(),
                fixture.Create<int>(),
                fixture.Create<decimal>(),
                fixture.Create<decimal>(),
                fixture.Create<DateTime>(),
                fixture.Create<DateTime>());

            var promotion1 = new Promotion(
               fixture.Create<IEnumerable<string>>(),
               fixture.Create<IEnumerable<string>>(),
               fixture.Create<IEnumerable<string>>(),
               fixture.Create<int>(),
               fixture.Create<decimal>(),
               fixture.Create<decimal>(),
               fixture.Create<DateTime>(),
               fixture.Create<DateTime>());
            var list = new List<Promotion>();
            list.Add(promotion);
            list.Add(promotion1);

            fixture.Inject(list);


          

        }
    }
}