using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Service_API;

var builder = Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

builder.Build().Run();