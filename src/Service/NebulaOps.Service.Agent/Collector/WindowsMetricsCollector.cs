using NebulaOps.Models.Metrics;
using NebulaOps.Service.Agent.Interfaces.Collector;

using System.Diagnostics;
using System.Net.NetworkInformation;

namespace NebulaOps.Service.Agent.Collector;
public class WindowsMetricsCollector : IMetricsCollector
{
    private PerformanceCounter cpuCounter = new("Processor", "% Processor Time", "_Total");
    private PerformanceCounter memCounter = new("Memory", "% Committed Bytes In Use");

    public HostMetrics Collect()
    {
        var cpu = cpuCounter.NextValue();
        var mem = memCounter.NextValue();
        var disk = GetAllDiskMetrics();
        var net = GetAllNetworkMetrics();

        return new HostMetrics
        {
            Hostname = Environment.MachineName,
            Timestamp = DateTime.UtcNow,
            Cpu = cpu,
            Memory = mem,
            Disk = disk,
            Network = net
        };
    }

    public static List<NetworkInterfaceMetrics> GetAllNetworkMetrics()
    {
        var metrics = new List<NetworkInterfaceMetrics>();

        foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            var stats = nic.GetIPv4Statistics();
            metrics.Add(new NetworkInterfaceMetrics
            {
                Name = nic.Name,
                BytesSent = stats.BytesSent,
                BytesReceived = stats.BytesReceived,
                PacketsSent = stats.UnicastPacketsSent,
                PacketsReceived = stats.UnicastPacketsReceived,
                ErrorsSent = stats.OutgoingPacketsWithErrors,
                ErrorsReceived = stats.IncomingPacketsWithErrors
            });
        }

        return metrics;
    }

    public static List<DiskMetrics> GetAllDiskMetrics()
    {
        var metrics = new List<DiskMetrics>();

        foreach (var drive in DriveInfo.GetDrives())
        {
            if (!drive.IsReady) continue;

            var used = drive.TotalSize - drive.TotalFreeSpace;
            metrics.Add(new DiskMetrics
            {
                Name = drive.Name,
                TotalSize = drive.TotalSize,
                FreeSpace = drive.TotalFreeSpace,
                UsagePercent = (double)used / drive.TotalSize * 100
            });
        }

        return metrics;
    }


}