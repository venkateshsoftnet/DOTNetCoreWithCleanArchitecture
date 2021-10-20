using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TFLAssessment.Application.Interfaces;
using TFLAssessment.Infrastructure.Shared.HttpClients;

namespace TFLAssessment.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddHttpClient<IRoadClient, RoadClient>();
        }
    }
}
