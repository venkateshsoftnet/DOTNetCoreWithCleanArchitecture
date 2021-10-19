using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TFL.Road.Status.Domain.Entities;

namespace TFL.Road.Status.Application.Interfaces
{
    public interface IRoadClient
    {
        Task<List<Roads>> GetRoadStatusAsync(RoadStatusQuery roadStatusQuery, CancellationToken cancellationToken);
    }
}
