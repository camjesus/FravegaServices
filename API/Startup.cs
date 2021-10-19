using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FravegaService.Models;
using Domain.Core.Services;
using Domain.Core.Data;
using Infrastucture.Data.Mongo.Options;
using Infrastucture.Data.Mongo;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mongoOptions = Configuration.GetSection(nameof(MongoOptions)).Get<MongoOptions>();

            services.AddRazorPages();
            //services.AddControllers().ConfigureApiBehaviorOptions(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});
            

            services.AddScoped<DataContext>()
                    .AddSingleton(sp => new MongoClient(sp.GetRequiredService<IOptionsSnapshot<MongoOptions>>().Value.ConnectionString))
                    .AddScoped<IAddPromocionEntityService, AddPromocionEntityService>()
                    .AddScoped<IDeletePromotionService, DeletePromotionService>()
                    .AddScoped<IValidarCuotasService, ValidarCuotasService>()
                    .AddScoped<IValidarPorcentajeService, ValidarPorcentajeService>()
                    .AddScoped<IValidarPromocionService, ValidarPromocionService>()
                    .AddScoped<IValidarExistenciaService, ValidarExistenciaService>()
                    .AddScoped<IValidarFechasService, ValidarFechasService>()
                    .AddScoped<IPromotionRepository, PromotionRepository>();

            MongoSetup.OnStartup();

        }

        private void AddSwaggerGen(Action<object> p)
        {
            throw new NotImplementedException();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
