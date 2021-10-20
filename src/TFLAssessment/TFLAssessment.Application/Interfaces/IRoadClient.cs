using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TFLAssessment.Domain.Entities;

namespace TFLAssessment.Application.Interfaces
{
    /// <summary>
    /// IRoldClient interface used for define the the RoadClient signatures
    /// </summary>
    public interface IRoadClient
    {
        Task<List<Road>> GetRoadStatusAsync(RoadStatusQuery roadStatusQuery, CancellationToken cancellationToken);
    }
}
