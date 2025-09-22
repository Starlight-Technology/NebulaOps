namespace NebulaOps.Models.Metrics;
public class ContainerMetrics
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Status { get; set; } = "";
    public double CpuPercent { get; set; }
    public double MemoryUsageMB { get; set; }
    public double MemoryLimitMB { get; set; }
    public double NetInputMB { get; set; }
    public double NetOutputMB { get; set; }
    public double BlockInputMB { get; set; }
    public double BlockOutputMB { get; set; }
}
