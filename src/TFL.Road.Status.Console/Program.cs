using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Threading.Tasks;
using TFL.Road.Status.Application;
using TFL.Road.Status.Application.Mappings;
using TFL.Road.Status.Infrastructure.Shared;

namespace TFL.Road.Status.Console
{
    public class Program
    {
        async static Task<int> Main(string[] args)
        {
            try
            {
                System.Console.ResetColor();

                if (args.Length != 1)
                {
                    return ExitCodes.BadInput;
                }

                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json");

                IConfigurationRoot configuration = builder.Build();

                IServiceCollection services = new ServiceCollection();
                services.AddSingleton<IConfiguration>(configuration);
                services.AddApplicationLayer();
                services.AddSharedInfrastructure();
                
                // Auto Mapper Configurations
                var mapperConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new RoadStatusProfile());
                });

                IMapper mapper = mapperConfig.CreateMapper();
                services.AddSingleton(mapper);

                var mediator = BuildMediator(services);

                var query = new RoadStatusQuery
                {
                    RoadId = args[0]
                };
                var apiResult = await mediator.Send(query);

                foreach (var result in apiResult)
                {
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine($"The status of the {result.DisplayName} is as follows:");
                    System.Console.WriteLine($"Road Status is {result.StatusSeverity}");
                    System.Console.WriteLine($"Road Status Description is {result.StatusSeverityDescription}");
                }

                return ExitCodes.Success;
            }
            catch (Exception ex)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                Log.Error($"Error : {ex.Message}");

                return ExitCodes.Failure;
            }

            System.Console.ReadLine();
        }

        private static IMediator BuildMediator(IServiceCollection serviceCollection)
        {
            var provider = serviceCollection.BuildServiceProvider();

            return provider.GetRequiredService<IMediator>();
        }
    }

    public static class ExitCodes
    {
        public static int Success => 0;
        public static int NotFound => 1;
        public static int BadInput => 2;
        public static int Failure => 3;
    }
}




