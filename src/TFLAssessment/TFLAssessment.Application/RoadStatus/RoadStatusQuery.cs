using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TFLAssessment.Application.Interfaces;
using TFLAssessment.Application.Mappings;

namespace TFLAssessment.Application
{
    public class RoadStatusQuery : IRequest<List<RoadStatusResponse>>
    {
        public IList<string> RoadIds { get; set; }

        public class RoadStatusQueryHandler : IRequestHandler<RoadStatusQuery, List<RoadStatusResponse>>
        {
            private readonly IRoadClient _roadClient;
            private readonly IRoadStatusResponseMapper _roadStatusResponseMapper;

            public RoadStatusQueryHandler(IRoadClient roadClient, IRoadStatusResponseMapper roadStatusResponseMapper)
            {
                _roadClient = roadClient;
                _roadStatusResponseMapper = roadStatusResponseMapper;
            }

            public async Task<List<RoadStatusResponse>> Handle(RoadStatusQuery request, CancellationToken cancellationToken)
            {
                var roads = await _roadClient.GetRoadStatusAsync(request, cancellationToken);

                var response = _roadStatusResponseMapper.MapToRoadStatusResponse(roads);

                return response;
            }
        }
    }
}

