using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;

namespace Common.Logging;

public static class Logging
{
    public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger => (context, loggerConfiguration) =>
    {
        var env = context.HostingEnvironment;
        loggerConfiguration.MinimumLevel.Information()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application Name", env.ApplicationName)
        .Enrich.WithProperty("Environment Name", env.EnvironmentName)
        .Enrich.WithExceptionDetails()
        .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Warning)
        .WriteTo.Console();


        if (context.HostingEnvironment.IsDevelopment())
        {
            loggerConfiguration.MinimumLevel.Override("Catalog", Serilog.Events.LogEventLevel.Debug);
            loggerConfiguration.MinimumLevel.Override("Basket", Serilog.Events.LogEventLevel.Debug);
            loggerConfiguration.MinimumLevel.Override("Discount", Serilog.Events.LogEventLevel.Debug);
            loggerConfiguration.MinimumLevel.Override("Ordering", Serilog.Events.LogEventLevel.Debug);
        }

        // ✅ Seq logging
        var seqServerUrl = context.Configuration.GetValue<string>("SeqConfiguration:Uri");
        if (!string.IsNullOrEmpty(seqServerUrl))
        {
            loggerConfiguration.WriteTo.Seq(seqServerUrl);
        }

        //Elastic Search
        //var elasticUrl = context.Configuration.GetValue<string>("ElasticConfiguration:URI");
        //if (!string.IsNullOrEmpty(elasticUrl))
        //{
        //    loggerConfiguration.WriteTo.Elasticsearch(
        //        new ElasticsearchSinkOptions(new Uri(elasticUrl))
        //        {
        //            AutoRegisterTemplate = true,
        //            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
        //            IndexFormat = "ecommerce-Logs-{0:yyyy.MM.dd}",
        //            MinimumLogEventLevel = LogEventLevel.Debug
        //        });
        //}
    };
}
