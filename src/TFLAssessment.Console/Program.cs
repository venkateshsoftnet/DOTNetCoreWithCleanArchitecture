using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;
using TFLAssessment.Application;
using TFLAssessment.Application.Exceptions;
using TFLAssessment.Application.Mappings;
using TFLAssessment.Domain.Entities;
using TFLAssessment.Infrastructure.Shared;
using static System.Console;

namespace TFLAssessment.Console
{
    /// <summary>
    /// Main Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// This method is an entry point of ASP.NET Core web applications
        /// </summary>
        /// <param name="args">Road Ids</param>
        /// <returns>Exit Code</returns>
        async static Task<int> Main(string[] args)
        {
            try
            {
                ResetColor();

                if (args.Length <= 0)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Road id argument is missing");
                    ResetColor();

                    return ExitCodes.BadInput;
                }

                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ApiExceptionHandler);

                IServiceCollection services = ConfigureService();
                MapperConfiguration(services);

                var provider = services.BuildServiceProvider();
                var mediator = provider.GetRequiredService<IMediator>();

                Log.Information("Starting application");
                var query = new RoadStatusQuery
                {
                    RoadIds = args
                };
                var apiResult = await mediator.Send(query);

                foreach (var result in apiResult)
                {
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine($"The status of the {result.DisplayName} is as follows:");
                    WriteLine($"Road Status is {result.StatusSeverity}");
                    WriteLine($"Road Status Description is {result.StatusSeverityDescription}");
                    WriteLine(Environment.NewLine);
                }

                ResetColor();
                Log.Information("Process completed successfully. {@Query}", apiResult);

                return ExitCodes.Success;
            }
            catch (ApiException ex)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine(ex.Message);
                Log.Error($"Failure encountered : {ex.Message}");
                ResetColor();

                return ExitCodes.Failure;
            }
            catch (Exception ex)
            {
                Log.Error($"Failure encountered : {ex.Message}");
                WriteLine("There was an error running the application.");

                return ExitCodes.Failure;
            }
        }

        /// <summary>
        /// This method is configure IServiceCollection.
        /// </summary>
        /// <returns>Exit Code</returns>
        private static IServiceCollection ConfigureService()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();

            //setup the rolling logfile
            var serilogLogger = new LoggerConfiguration()
              .ReadFrom.Configuration(configuration)
              .CreateLogger();

            IServiceCollection services = new ServiceCollection();
            services.Configure<ApiSettings>(configuration.GetSection(nameof(ApiSettings)));
            services.AddOptions();
            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddSerilog(logger: serilogLogger, dispose: true);
            });
            services.AddSingleton<IConfiguration>(configuration);
            services.AddApplicationLayer();
            services.AddSharedInfrastructure();

            return services;
        }

        private static void MapperConfiguration(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new RoadStatusProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        static void ApiExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var ex = (Exception)args.ExceptionObject;
            Log.Error($"Failure encountered : {ex.Message}");

            Environment.ExitCode = 1;
        }
    }
}