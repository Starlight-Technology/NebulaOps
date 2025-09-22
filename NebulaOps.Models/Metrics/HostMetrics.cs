using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaOps.Models.Metrics;
public class HostMetrics
{
    public string Hostname { get; set; } = "";
    public DateTime Timestamp { get; set; }
    public double Cpu { get; set; }
    public double Memory { get; set; }
    public List<DiskMetrics>? Disk { get; set; }
    public List<NetworkInterfaceMetrics>? Network { get; set; }
    public List<ContainerMetrics>? Containers { get; set; }
}