using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TFL.Road.Status.Application;

namespace TFL.Road.Status.Console
{
    public class MediatRService : IMediatRService
    {
        private readonly IMediator mediator;
        public MediatRService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task SendRequest()
        {
            //var request = new RoadStatusQuery
            //{
            //    RoadIds = _config.RoadIds,
            //    ApiUri = _config.ApiUrl,
            //    AppId = _config.AppId,
            //    ApiKey = _config.ApiKey
            //};

            //var response = await this.mediator.Send(request, stoppingToken);

        }
    }
}
