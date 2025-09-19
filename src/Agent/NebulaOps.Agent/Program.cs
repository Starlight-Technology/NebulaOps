using NebulaOps.Service.Agent.Collector;
using NebulaOps.Service.Agent.Interfaces.Collector;

using System.Runtime.InteropServices;

Console.WriteLine("Initializing collector agent.");

IMetricsCollector collector = RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
    ? new LinuxMetricsCollector()
    : new WindowsMetricsCollector();

Console.WriteLine("NebulaOps Agent started...");

while (true)
{
    var metrics = collector.Collect();
    Console.WriteLine($"[{metrics.Timestamp}] {metrics.Hostname} -> CPU: {metrics.Cpu:F1}%");
    await Task.Delay(1000);
}
