using FravegaService.Infrastructure;
using FravegaService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;
using System;

namespace FravegaService
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
            services.AddRazorPages();
            services.AddControllers();
            services.AddScoped<AppDbContext>();
            //services
            services.AddScoped<IAddPromocionEntityService, AddPromocionEntityService>();
                    //.AddScoped<IGetPromocionByIdService, GetPromocionByIdService>()
                    //.AddScoped<IGetPromocionesService, GetPromocionesService>()
                    //.AddScoped<IGetPromocionesVigentesService, GetPromocionesVigentesService>()
                    //.AddScoped<IUpdatePromocionService, UpdatePromocionService>();
            AddSwagger(services);
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"test fravega {groupName}",
                    Version = groupName,
                    Description = "test fravega",
                    Contact = new OpenApiContact
                    {
                        Name = "test",
                        Email = string.Empty,
                        Url = new Uri(""),
                    }
                });
            });
        }

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