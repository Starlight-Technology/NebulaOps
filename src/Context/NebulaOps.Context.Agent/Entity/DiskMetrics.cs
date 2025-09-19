using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaOps.Context.Agent.Entity;
public class DiskMetrics
{
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("total_size")]
    public long TotalSize { get; set; }

    [BsonElement("free_space")]
    public long FreeSpace { get; set; }

    [BsonElement("usage_percent")]
    public double UsagePercent { get; set; }
}
