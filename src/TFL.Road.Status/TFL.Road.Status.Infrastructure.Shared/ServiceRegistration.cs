using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TFL.Road.Status.Application.Interfaces;
using TFL.Road.Status.Infrastructure.Shared.HttpClients;

namespace TFL.Road.Status.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //IValidator<RoadStatusQuery>
            //services.AddTransient<IValidator<RoadStatusQuery>, RoadStatusQueryValidator>();
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddTransient<IRoadClient, RoadClient>();
            services.AddHttpClient<IRoadClient, RoadClient>();
        }
    }
}
