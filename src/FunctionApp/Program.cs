﻿using TheGnouCommunity.UrlManager.Application.Extensions;
using TheGnouCommunity.UrlManager.Infrastructure;
using TheGnouCommunity.UrlManager.Infrastructure.Extensions;
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
    services.AddInfrastructure(_ => _.Bind(context.Configuration.GetSection(StorageOptions.ConfigurationSectionName))
                                     .ValidateDataAnnotations());
});

var host = hostBuilder.Build();

await host.RunAsync();