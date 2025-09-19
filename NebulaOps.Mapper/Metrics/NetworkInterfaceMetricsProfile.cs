using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaOps.Mapper.Metrics;
public class NetworkInterfaceMetricsProfile : Profile
{
    public NetworkInterfaceMetricsProfile()
    {
        CreateMap<Context.Agent.Entity.NetworkInterfaceMetrics, Models.Metrics.NetworkInterfaceMetrics>().ReverseMap();
    }
}
