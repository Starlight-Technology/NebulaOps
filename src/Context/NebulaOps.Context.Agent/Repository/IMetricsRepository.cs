using NebulaOps.Context.Agent.Entity;

namespace NebulaOps.Context.Agent.Repository;
public interface IMetricsRepository
{
    Task<List<HostMetrics>> GetAll();
    Task<List<HostMetrics>> GetLatestAsync(string hostname, int limit = 10);
    Task InsertAsync(HostMetrics metrics);
}
