using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheGnouCommunity.UrlManager.Application.Commands;
using TheGnouCommunity.UrlManager.Application.Extensions;
using TheGnouCommunity.UrlManager.Infrastructure.Extensions;

var hostBuilder = new HostBuilder();

hostBuilder.ConfigureFunctionsWebApplication();

hostBuilder.ConfigureAppConfiguration((context, builder) =>
{
    if (context.HostingEnvironment.IsDevelopment())
    {
        builder.AddUserSecrets<Program>();
    }
});

hostBuilder.ConfigureServices((context, services) =>
{
    services.AddApplication(_ => _.Bind(context.Configuration.GetSection(GeoIP2Options.ConfigurationSectionName))
                                  .ValidateDataAnnotations());
    services.AddInfrastructure(context.Configuration["AzureWebJobsStorage"]);
});

var host = hostBuilder.Build();

await host.RunAsync();