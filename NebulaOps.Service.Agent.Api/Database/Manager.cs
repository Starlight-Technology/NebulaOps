using AutoMapper;

using NebulaOps.Context.Agent.Entity;
using NebulaOps.Context.Agent.Repository;
using NebulaOps.Models.Metrics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NebulaOps.Service.Agent.Api.Database;
public class Manager(IMetricsRepository metricsRepository, IMapper mapper)
: IManager
{
    public async Task SaveMetricsAsync(Models.Metrics.HostMetrics metrics)
    {
        var entity = mapper.Map<Context.Agent.Entity.HostMetrics>(metrics);

        await metricsRepository.InsertAsync(entity);
    }

    public async Task<ICollection<Models.Metrics.HostMetrics>> GetHostMetricsAsync()
    {
        var entities = await metricsRepository.GetAll();
        return mapper.Map<ICollection<Models.Metrics.HostMetrics>>(entities);
    }
}
