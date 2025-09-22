using MongoDB.Driver;

using NebulaOps.Context.Agent.Entity;

namespace NebulaOps.Context.Agent.Repository;
public class MetricsRepository : IMetricsRepository
{
    private readonly IMongoCollection<HostMetrics> _collection;

    public MetricsRepository(MongoAgentContext context)
    {
        _collection = context.Metrics;
    }

    public async Task InsertAsync(HostMetrics metrics)
    {
        await _collection.InsertOneAsync(metrics);
    }

    public async Task<List<HostMetrics>> GetLatestAsync(string hostname, int limit = 10)
    {
        var filter = Builders<HostMetrics>.Filter.Eq(m => m.Hostname, hostname);
        var sort = Builders<HostMetrics>.Sort.Descending(m => m.Timestamp);

        return await _collection.Find(filter).Sort(sort).Limit(limit).ToListAsync();
    }

    public async Task<List<HostMetrics>> GetAll(DateTime? start = null, DateTime? end = null)
    {
        // Define valores padrão se necessário
        start ??= DateTime.UtcNow.AddDays(-7);
        end ??= DateTime.UtcNow;

        // Cria filtro por intervalo de tempo
        var filter = Builders<HostMetrics>.Filter.And(
            Builders<HostMetrics>.Filter.Gte(m => m.Timestamp, start),
            Builders<HostMetrics>.Filter.Lte(m => m.Timestamp, end)
        );

        var sort = Builders<HostMetrics>.Sort.Ascending(m => m.Timestamp);

        return await _collection.Find(filter).Sort(sort).ToListAsync();
    }
}
