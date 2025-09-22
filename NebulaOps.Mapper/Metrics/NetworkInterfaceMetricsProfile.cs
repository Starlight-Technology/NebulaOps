using AutoMapper;

namespace NebulaOps.Mapper.Metrics;
public class NetworkInterfaceMetricsProfile : Profile
{
    public NetworkInterfaceMetricsProfile()
    {
        CreateMap<Context.Agent.Entity.NetworkInterfaceMetrics, Models.Metrics.NetworkInterfaceMetrics>().ReverseMap();
    }
}
