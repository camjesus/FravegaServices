using Infrastucture.Data.Mongo;
using Infrastucture.Data.Mongo.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var mongoClient = host.Services.GetRequiredService<MongoClient>();
                var options = host.Services.GetRequiredService<IOptions<MongoOptions>>();
                //var dataContext = host.Services.GetRequiredService<DataContext>(); //var dataContext = new DataContext();
                var dataContext = new DataContext(mongoClient, options);
                try
                {
                    await dataContext.CreateIndexesAsync();
                }
                catch (System.Exception ex)
                {
                    var a = ex;
                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}