using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaOps.Models.Metrics;
public class NetworkInterfaceMetrics
{
    public string Name { get; set; } = string.Empty;
    public long BytesSent { get; set; }
    public long BytesReceived { get; set; }
    public long PacketsSent { get; set; }
    public long PacketsReceived { get; set; }
    public long ErrorsSent { get; set; }
    public long ErrorsReceived { get; set; }
}
