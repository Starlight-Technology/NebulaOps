using NebulaOps.Models.Metrics;
using NebulaOps.Service.Agent.Interfaces.Collector;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NebulaOps.Service.Agent.Collector;
public class LinuxMetricsCollector : IMetricsCollector
{
    public HostMetrics Collect()
    {
        var cpu = GetCpuUsage();
        var mem = GetMemoryUsage();
        var disk = GetAllDiskMetrics();
        var net = GetAllNetworkMetrics();
        var containers = GetDockerContainerMetrics();

        return new HostMetrics
        {
            Hostname = Environment.MachineName,
            Timestamp = DateTime.UtcNow,
            Cpu = cpu,
            Memory = mem,
            Disk = disk,
            Network = net,
            Containers = containers
        };
    }

    private double GetCpuUsage()
    {
        var cpuLine1 = System.IO.File.ReadAllText("/proc/stat").Split('\n').First();
        var cpuTimes1 = cpuLine1.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse).ToArray();
        var idle1 = cpuTimes1[3];
        var total1 = cpuTimes1.Sum();

        Thread.Sleep(500);

        var cpuLine2 = System.IO.File.ReadAllText("/proc/stat").Split('\n').First();
        var cpuTimes2 = cpuLine2.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse).ToArray();
        var idle2 = cpuTimes2[3];
        var total2 = cpuTimes2.Sum();

        var idleDelta = idle2 - idle1;
        var totalDelta = total2 - total1;

        return (1.0 - (double)idleDelta / totalDelta) * 100.0;
    }

    private double GetMemoryUsage()
    {
        var memInfo = System.IO.File.ReadAllLines("/proc/meminfo");
        var total = double.Parse(memInfo.First(l => l.StartsWith("MemTotal")).Split(':')[1].Trim().Split(' ')[0]);
        var free = double.Parse(memInfo.First(l => l.StartsWith("MemAvailable")).Split(':')[1].Trim().Split(' ')[0]);
        return ((total - free) / total) * 100;
    }


    private List<DiskMetrics> GetAllDiskMetrics()
    {
        var result = new List<DiskMetrics>();
        var output = RunCommand("df -B1 --output=source,size,avail,pcent -x tmpfs -x devtmpfs");

        var lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries).Skip(1); // pula cabeçalho
        foreach (var line in lines)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 4)
            {
                var name = parts[0];
                if (!long.TryParse(parts[1], out var total)) continue;
                if (!long.TryParse(parts[2], out var free)) continue;
                var usagePercent = double.Parse(parts[3].Trim('%'));

                result.Add(new DiskMetrics
                {
                    Name = name,
                    TotalSize = total,
                    FreeSpace = free,
                    UsagePercent = usagePercent
                });
            }
        }
        return result;
    }


    private List<NetworkInterfaceMetrics> GetAllNetworkMetrics()
    {
        var result = new List<NetworkInterfaceMetrics>();
        var lines = System.IO.File.ReadAllLines("/proc/net/dev").Skip(2); // pula cabeçalho

        foreach (var line in lines)
        {
            var parts = line.Split(':');
            if (parts.Length != 2) continue;

            var name = parts[0].Trim();
            var values = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (values.Length >= 16)
            {
                result.Add(new NetworkInterfaceMetrics
                {
                    Name = name,
                    BytesReceived = long.Parse(values[0]),
                    PacketsReceived = long.Parse(values[1]),
                    ErrorsReceived = long.Parse(values[2]),
                    BytesSent = long.Parse(values[8]),
                    PacketsSent = long.Parse(values[9]),
                    ErrorsSent = long.Parse(values[10])
                });
            }
        }
        return result;
    }

    private string RunCommand(string cmd)
    {
        var psi = new ProcessStartInfo("bash", $"-c \"{cmd}\"")
        {
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        using var process = Process.Start(psi);
        return process?.StandardOutput.ReadToEnd() ?? "";
    }

    private List<ContainerMetrics> GetDockerContainerMetrics()
    {
        var result = new List<ContainerMetrics>();
        var output = RunCommand("docker stats --no-stream --format \"{{.Container}}|{{.Name}}|{{.CPUPerc}}|{{.MemUsage}}|{{.NetIO}}|{{.BlockIO}}\"");

        foreach (var line in output.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = line.Split('|');
            if (parts.Length != 6) continue;

            var memParts = parts[3].Split('/');
            var netParts = parts[4].Split('/');
            var blockParts = parts[5].Split('/');

            result.Add(new ContainerMetrics
            {
                Id = parts[0],
                Name = parts[1],
                CpuPercent = double.Parse(parts[2].Trim('%')),
                MemoryUsageMB = ParseSize(memParts[0]),
                MemoryLimitMB = ParseSize(memParts[1]),
                NetInputMB = ParseSize(netParts[0]),
                NetOutputMB = ParseSize(netParts[1]),
                BlockInputMB = ParseSize(blockParts[0]),
                BlockOutputMB = ParseSize(blockParts[1])
            });
        }

        return result;
    }

    private double ParseSize(string raw)
    {
        raw = raw.Trim().ToUpperInvariant();
        if (raw.EndsWith("KB")) return double.Parse(raw[..^2]) / 1024;
        if (raw.EndsWith("MB")) return double.Parse(raw[..^2]);
        if (raw.EndsWith("GB")) return double.Parse(raw[..^2]) * 1024;
        if (raw.EndsWith("B")) return double.Parse(raw[..^1]) / (1024 * 1024);
        return 0;
    }

}