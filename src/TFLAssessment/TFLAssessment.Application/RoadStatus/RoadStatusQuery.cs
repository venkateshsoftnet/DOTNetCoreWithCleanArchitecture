using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TFLAssessment.Application.Interfaces;

namespace TFLAssessment.Application
{
    public class RoadStatusQuery : IRequest<List<RoadStatusResponse>>
    {
        public IList<string> RoadIds { get; set; }

        public class RoadStatusQueryHandler : IRequestHandler<RoadStatusQuery, List<RoadStatusResponse>>
        {
            private readonly IRoadClient _roadClient;
            private readonly IMapper _mapper;

            public RoadStatusQueryHandler(IRoadClient roadClient, IMapper mapper)
            {
                _roadClient = roadClient;
                _mapper = mapper;
            }

            public async Task<List<RoadStatusResponse>> Handle(RoadStatusQuery request, CancellationToken cancellationToken)
            {
                var roads = await _roadClient.GetRoadStatusAsync(request, cancellationToken);

                var response = _mapper.Map<List<RoadStatusResponse>>(roads);

                return response;
            }
        }
    }
}

