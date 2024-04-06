using Application.Extensions;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
    services.AddApplication();
    services.AddInfrastructure(_ => _.Bind(context.Configuration.GetSection(TableStorageOptions.ConfigurationSectionName))
                                     .ValidateDataAnnotations());
});

var host = hostBuilder.Build();

await host.RunAsync();