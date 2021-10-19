using AutoMapper;

using TFL.Road.Status.Domain.Entities;

namespace TFL.Road.Status.Application.Mappings
{
    public class RoadStatusProfile : Profile
    {
        public RoadStatusProfile()
        {
            CreateMap<Roads, RoadStatusResponse>();
        }
    }
}
