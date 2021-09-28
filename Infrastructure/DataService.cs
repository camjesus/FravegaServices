using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace FravegaService.Infrastructure
{
    public class DataService
    {
        public void ConfigureServices(IServiceCollection service)
        {
            service.AddSingleton<IMongoClient>(s => new MongoClient(s.GetRequiredService<IConfiguration>()["MongoDb"]));
            service.AddScoped(s => new AppDbContext(s.GetRequiredService<IMongoClient>(), s.GetRequiredService<IConfiguration>()["DbName"]));
        }
    }
}

