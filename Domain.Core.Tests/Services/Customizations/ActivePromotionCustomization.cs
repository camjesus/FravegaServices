using AutoFixture;
using FravegaService.Models;
using System;
using System.Collections.Generic;

namespace Domain.Core.Tests.Services.Customizations
{
    public class ActivePromotionCustomization : ICustomization
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

            fixture.Inject(promotion);
        }
    }
}