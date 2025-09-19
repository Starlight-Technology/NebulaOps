using System;
using System.Linq;

namespace NebulaOps.Service.Agent.Api.Database;
public interface IManager
{
    Task<ICollection<Models.Metrics.HostMetrics>> GetHostMetricsAsync();
    Task SaveMetricsAsync(Models.Metrics.HostMetrics metrics);
}
