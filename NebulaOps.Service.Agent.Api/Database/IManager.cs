using NebulaOps.Utils;

using System;
using System.Linq;

namespace NebulaOps.Service.Agent.Api.Database;
public interface IManager
{
    Task<DefaultResponse> GetHostMetricsAsync(DateTime? start = null, DateTime? end = null);
    Task<DefaultResponse> SaveMetricsAsync(Models.Metrics.HostMetrics metrics);
}
