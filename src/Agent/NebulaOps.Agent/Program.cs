using NebulaOps.Service.Agent.Collector;
using NebulaOps.Service.Agent.Interfaces.Collector;

using System.Net.Http.Json;
using System.Runtime.InteropServices;

Console.WriteLine("Initializing collector agent.");

IMetricsCollector collector = RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
    ? new LinuxMetricsCollector()
    : new WindowsMetricsCollector();

Console.WriteLine("NebulaOps Agent started...");

HttpClient httpClient = new();


while (true)
{
    var metrics = collector.Collect();
    Console.WriteLine($"[{metrics.Timestamp}] {metrics.Hostname} -> CPU: {metrics.Cpu:F1}%");
    await httpClient.PostAsJsonAsync("http://localhost:5106/", metrics);
    await Task.Delay(1000);
}
