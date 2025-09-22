using MongoDB.Bson.Serialization.Attributes;

namespace NebulaOps.Context.Agent.Entity;
public class ContainerMetrics
{
    [BsonElement("id")]
    public string Id { get; set; } = "";

    [BsonElement("name")]
    public string Name { get; set; } = "";

    [BsonElement("status")]
    public string Status { get; set; } = "";

    [BsonElement("cpu_percent")]
    public double CpuPercent { get; set; }

    [BsonElement("memory_usage_mb")]
    public double MemoryUsageMB { get; set; }

    [BsonElement("memory_limit_mb")]
    public double MemoryLimitMB { get; set; }

    [BsonElement("net_input_mb")]
    public double NetInputMB { get; set; }

    [BsonElement("net_output_mb")]
    public double NetOutputMB { get; set; }

    [BsonElement("block_input_mb")]
    public double BlockInputMB { get; set; }

    [BsonElement("block_output_mb")]
    public double BlockOutputMB { get; set; }
}
