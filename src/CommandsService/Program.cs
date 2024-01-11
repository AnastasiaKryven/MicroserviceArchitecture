using CommandsService.AsyncDataServices;
using CommandsService.Data;
using CommandsService.EventProcessing;
using CommandsService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

namespace CommandsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<ICommandRepository, CommandRepository>();

            builder.Services.AddScoped<IPlatformDataClient, PlatformDataClient>();

            builder.Services.AddSingleton<IEventProcessor, EventProcessor>();

            builder.Services.AddHostedService<MessageBusSubscriber>();

            var app = builder.Build();

            app.UseAuthorization();

            app.MapControllers();

            PrepDb.PrepPopulation(app);

            app.Run();
        }
    }
}
