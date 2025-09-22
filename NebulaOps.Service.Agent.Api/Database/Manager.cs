using AutoMapper;

using NebulaOps.Context.Agent.Entity;
using NebulaOps.Context.Agent.Repository;
using NebulaOps.Models.Metrics;
using NebulaOps.Utils;

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
    public async Task<DefaultResponse> SaveMetricsAsync(Models.Metrics.HostMetrics metrics)
    {
        try
        {
            var entity = mapper.Map<Context.Agent.Entity.HostMetrics>(metrics);

            await metricsRepository.InsertAsync(entity);
            return new DefaultResponse();
        }
        catch (Exception ex)
        {
            return new DefaultResponse(httpStatus: System.Net.HttpStatusCode.BadRequest, message: ex.Message);
        }


    }

    public async Task<DefaultResponse> GetHostMetricsAsync()
    {
        try
        {
            var entities = await metricsRepository.GetAll();
            var obj = mapper.Map<ICollection<Models.Metrics.HostMetrics>>(entities);
            return new DefaultResponse(objectResponse: obj);
        }
        catch (Exception ex)
        {
            return new DefaultResponse(httpStatus: System.Net.HttpStatusCode.BadRequest, message: ex.Message);
        }
    }
}
