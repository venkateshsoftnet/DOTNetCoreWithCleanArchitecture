using AutoMapper;
using System.Collections.Generic;
using TFLAssessment.Domain.Entities;

namespace TFLAssessment.Application.Mappings
{
    public interface IRoadStatusResponseMapper
    {
        List<RoadStatusResponse> MapToRoadStatusResponse(List<Road> road);
    }
}
