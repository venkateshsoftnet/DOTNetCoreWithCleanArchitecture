using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TFL.Road.Status.Domain.Entities;

namespace TFL.Road.Status.Application.Interfaces
{
    public interface IRoadService
    {
        Task<List<Roads>> GetRoadStatusAsync(string roadId, CancellationToken cancellationToken);
    }
}
