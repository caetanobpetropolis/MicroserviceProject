using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Amazon.SQS;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using OrderProcessor.OrderProcessorService;
using OrderProcessor.OrderProcessorService.Interfaces;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("appsettings.json", optional: false);
    })
    .ConfigureServices((context, services) =>
    {
        var awsOptions = context.Configuration.GetAWSOptions();
        awsOptions.Credentials = new BasicAWSCredentials(
            context.Configuration["AWS:AccessKey"],
            context.Configuration["AWS:SecretKey"]
        );

        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonSQS>();
        services.AddSingleton<IOrderProcessorService, OrderProcessorService>();
    })
    .Build();

var processor = host.Services.GetRequiredService<IOrderProcessorService>();
await processor.StartAsync();