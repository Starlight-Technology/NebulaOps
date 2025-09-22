using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

using NebulaOps.Context.Agent;
using NebulaOps.Context.Agent.Repository;
using NebulaOps.Service.Agent.Api.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<NebulaOps.Mapper.Metrics.DiskMetricsProfile>();
    cfg.AddProfile<NebulaOps.Mapper.Metrics.HostMetricsProfile>();
    cfg.AddProfile<NebulaOps.Mapper.Metrics.NetworkInterfaceMetricsProfile>();
});

builder.Services.AddScoped<MongoAgentContext>();
builder.Services.AddScoped<IManager, Manager>();
builder.Services.AddScoped<IMetricsRepository, MetricsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapPost("/", async (IManager manager , [FromBody] NebulaOps.Models.Metrics.HostMetrics metrics) =>
{
 
    return await manager.SaveMetricsAsync(metrics).ConfigureAwait(true);
})
.WithName("PostMetrics");

app.MapGet("/", async (IManager manager, DateTime? start = null, DateTime? end = null) => 
{
    return await manager.GetHostMetricsAsync(start, end);
})
.WithName("GetMetrics");

await app.RunAsync().ConfigureAwait(true);
