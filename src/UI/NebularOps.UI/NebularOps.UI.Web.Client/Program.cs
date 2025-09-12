using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// registrar serviços MudBlazor
builder.Services.AddMudServices();

await builder.Build().RunAsync();
