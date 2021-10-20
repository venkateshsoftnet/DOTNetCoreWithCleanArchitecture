using AutoMapper;

using TFLAssessment.Domain.Entities;

namespace TFLAssessment.Application.Mappings
{
    /// <summary>
    /// This class is used for define the mapping definitions for RoadStatus
    /// </summary>
    public class RoadStatusProfile : Profile
    {
        /// <summary>
        /// This method is used for create mapping between Road and RoadStatusResponse
        /// </summary>
        public RoadStatusProfile()
        {
            CreateMap<Road, RoadStatusResponse>();
        }
    }
}
