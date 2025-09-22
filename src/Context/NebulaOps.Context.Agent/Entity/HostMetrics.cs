using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NebulaOps.Context.Agent.Entity;
public class HostMetrics
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("hostname")]
    public string Hostname { get; set; } = string.Empty;

    [BsonElement("timestamp")]
    public DateTime Timestamp { get; set; }

    [BsonElement("cpu")]
    public double Cpu { get; set; }

    [BsonElement("memory")]
    public double Memory { get; set; }

    [BsonElement("disk")]
    public List<DiskMetrics> Disk { get; set; }

    [BsonElement("network")]
    public List<NetworkInterfaceMetrics> Network { get; set; }

    [BsonElement("containers")]
    public List<ContainerMetrics>? Containers { get; set; }
}
