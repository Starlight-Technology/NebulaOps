using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

using NebulaOps.Service.Agent.Api.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<NebulaOps.Mapper.Metrics.DiskMetricsProfile>();
    cfg.AddProfile<NebulaOps.Mapper.Metrics.HostMetricsProfile>();
    cfg.AddProfile<NebulaOps.Mapper.Metrics.NetworkInterfaceMetricsProfile>();
});

builder.Services.AddScoped<IManager, Manager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapPost("/", (IManager manager ) =>
{
 
    return Results.Ok("Hello World!");
})
.WithName("PostMetrics");

await app.RunAsync().ConfigureAwait(true);
