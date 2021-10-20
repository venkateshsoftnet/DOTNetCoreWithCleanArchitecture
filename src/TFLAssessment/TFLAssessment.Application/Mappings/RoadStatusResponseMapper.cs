using AutoMapper;
using System.Collections.Generic;
using TFLAssessment.Domain.Entities;

namespace TFLAssessment.Application.Mappings
{
    public class RoadStatusResponseMapper : IRoadStatusResponseMapper
    {
        private readonly IMapper _mapper;

        public RoadStatusResponseMapper(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public List<RoadStatusResponse> MapToRoadStatusResponse(List<Road> roads)
        {
            return _mapper.Map<List<RoadStatusResponse>>(roads);
        }
    }
}
