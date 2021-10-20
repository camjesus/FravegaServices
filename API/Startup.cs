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
using Microsoft.AspNetCore.Mvc.Versioning;
using API.Controller;
using Infrastucture.Data.Mongo.Repositories;
using API.Middlewares;

namespace API
{
    public class Startup
    {
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mongoOptions = Configuration.GetSection(nameof(MongoOptions)).Get<MongoOptions>();
            
            services
                .AddOptions()
                .Configure<MongoOptions>(Configuration.GetSection(nameof(MongoOptions)))
                .AddScoped<DataContext>()
                .AddSingleton(sp => new MongoClient(mongoOptions.ConnectionString))
                .AddScoped<IAddPromocionEntityService, AddPromocionEntityService>()
                .AddScoped<IDeletePromotionService, DeletePromotionService>()
                .AddScoped<IGetPromocionByIdService, GetPromocionByIdService>()
                .AddScoped<IGetPromocionesService, GetPromocionesService>()
                .AddScoped<IGetPromocionesVigentesService, GetPromocionesVigentesService>()
                .AddScoped<IValidarCuotasService, ValidarCuotasService>()
                .AddScoped<IValidarPorcentajeService, ValidarPorcentajeService>()
                .AddScoped<IValidarPromocionService, ValidarPromocionService>()
                .AddScoped<IValidarExistenciaService, ValidarExistenciaService>()
                .AddScoped<IValidarFechasService, ValidarFechasService>()
                .AddScoped<IUpdatePromocionService, UpdatePromocionService>()
                .AddScoped<IUpdateVigenciaService, UpdateVigenciaService>()
                .AddScoped<IPromotionRepository, PromotionRepository>();
                
             services.AddApiVersioning(options =>
             {
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);

                options.Conventions.Controller<PromotionController>().HasApiVersion(new ApiVersion(1, 0));
             })
                .AddSwaggerGen(c =>
              {
                  c.CustomSchemaIds(x => x.FullName);
                  c.SwaggerDoc("v1", new OpenApiInfo
                  {
                      Version = "v1",
                      Title = "OMS Clients auth module",
                      Description = "OMS Clients auth module"
                  });
              })
                .AddMvc(options =>
                {
                    //options.UseCentralRoutePrefix(new RouteAttribute("v{version:apiVersion}"));
                    options.Filters.Add(new ProducesAttribute("application/json"));
                });

            MongoSetup.OnStartup();
        }

        private void AddSwaggerGen(Action<object> p)
        {
            throw new NotImplementedException();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStatusCodePages();

            app
              .UseEndpoints(endpoints =>
              {
                  endpoints.MapControllers();
              })
                .UseSwagger(c =>
                {
                    c.RouteTemplate = "help/docs/{documentName}/swagger.json";

                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        if (httpReq.Headers.ContainsKey("x-base-path"))
                        {
                            swaggerDoc.Servers = new List<OpenApiServer>
                            {
                                new OpenApiServer
                                {
                                    Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/{httpReq.Headers["x-base-path"].ToString().Trim('/')}"
                                }
                            };
                        }
                    });
                })
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("docs/v1/swagger.json", "OMS clients auth module V1");
                    c.RoutePrefix = "help";
                    c.DefaultModelsExpandDepth(-1);
                });
        }
    }
}
