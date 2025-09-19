using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaOps.Context.Agent.Entity;
public class NetworkInterfaceMetrics
{
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("bytes_sent")]
    public long BytesSent { get; set; }

    [BsonElement("bytes_received")]
    public long BytesReceived { get; set; }

    [BsonElement("packets_sent")]
    public long PacketsSent { get; set; }

    [BsonElement("packets_received")]
    public long PacketsReceived { get; set; }

    [BsonElement("errors_sent")]
    public long ErrorsSent { get; set; }

    [BsonElement("errors_received")]
    public long ErrorsReceived { get; set; }
}
