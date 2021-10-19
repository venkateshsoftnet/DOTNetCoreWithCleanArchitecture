using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using TFL.Road.Status.Application;

namespace TFL.Road.Status.Console
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }
        
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationRoot>(Configuration);
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        }

        public IMediator BuildMediator()
        {
            var services = new ServiceCollection();

            services.AddMediatR(new Type[] { typeof(RoadStatusQuery)});

            var provider = services.BuildServiceProvider();

            return provider.GetRequiredService<IMediator>();
        }
    }
}
