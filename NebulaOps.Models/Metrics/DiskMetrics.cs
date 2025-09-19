using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaOps.Models.Metrics;
public class DiskMetrics
{
    public string Name { get; set; } = string.Empty;
    public long TotalSize { get; set; }
    public long FreeSpace { get; set; }
    public double UsagePercent { get; set; }
}
