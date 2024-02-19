using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

namespace PlatformService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            if (builder.Environment.IsProduction())
            {
                Console.WriteLine("--> Using SQL Server DB!");
                builder.Services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
            }
            else
            {
                //Console.WriteLine("--> Using InMem DB!");
                //builder.Services.AddDbContext<AppDbContext>(opt =>
                //opt.UseInMemoryDatabase("InMem"));

                builder.Services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
            }

            builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

            builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

            builder.Services.AddGrpc();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            
            app.MapControllers();

            //PrepDb.PrepPopulation(app, builder.Environment.IsProduction());

            Console.WriteLine(app.Configuration["CommandService"]);

            app.Run();
        }
    }
}
