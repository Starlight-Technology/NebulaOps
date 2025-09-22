using MongoDB.Driver;

using NebulaOps.Context.Agent.Entity;

namespace NebulaOps.Context.Agent;

public class MongoAgentContext
{
    private readonly IMongoDatabase _database;

    public MongoAgentContext(string dbName = "NebulaOps")
    {
        var client = new MongoClient("mongodb://localhost:27017/?retryWrites=true&loadBalanced=false&serverSelectionTimeoutMS=5000&connectTimeoutMS=10000");
        _database = client.GetDatabase(dbName);
    }

    public IMongoCollection<HostMetrics> Metrics =>
        _database.GetCollection<HostMetrics>("Metrics");
}
