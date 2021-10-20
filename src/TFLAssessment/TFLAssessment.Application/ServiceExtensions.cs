using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TFLAssessment.Application.Mappings;

namespace TFLAssessment.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IRoadStatusResponseMapper), typeof(RoadStatusResponseMapper));
            services.AddMediatR(Assembly.GetExecutingAssembly());       
        }
    }
}
