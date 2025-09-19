using MongoDB.Driver;

using NebulaOps.Context.Agent.Entity;

namespace NebulaOps.Context.Agent;

public class MongoAgentContext
{
    private readonly IMongoDatabase _database;

    public MongoAgentContext(string connectionString, string dbName = "NebulaOps")
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(dbName);
    }

    public IMongoCollection<HostMetrics> Metrics =>
        _database.GetCollection<HostMetrics>("Metrics");
}
