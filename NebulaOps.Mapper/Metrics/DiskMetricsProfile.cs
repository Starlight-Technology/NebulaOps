using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaOps.Mapper.Metrics;
public class DiskMetricsProfile : Profile
{
    public DiskMetricsProfile()
    {
        CreateMap<Context.Agent.Entity.DiskMetrics, Models.Metrics.DiskMetrics>().ReverseMap();
    }
}
