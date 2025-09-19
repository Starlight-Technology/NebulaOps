using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaOps.Mapper.Metrics;
public class HostMetricsProfile:Profile
{
    public HostMetricsProfile()
    {
        CreateMap<Context.Agent.Entity.HostMetrics, Models.Metrics.HostMetrics>().ReverseMap();
    }
}
