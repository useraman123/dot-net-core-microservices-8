using AuthServer;
using AuthServer.Pages;
using Serilog;

Serilog.Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Serilog.Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    //builder.Services.AddIdentityServer()
    //        .AddInMemoryApiResources(Config.ApiResources)
    //        .AddInMemoryApiScopes(Config.ApiScopes)
    //        .AddTestUsers(TestUsers.Users)
    //        .AddInMemoryClients(Config.Clients)
    //        .AddDeveloperSigningCredential(); // only for dev use


    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    // this seeding is only for the template to bootstrap the DB and users.
    // in production you will likely want a different approach.
    if (args.Contains("/seed"))
    {
        Serilog.Log.Information("Seeding database...");
        SeedData.EnsureSeedData(app);
        Serilog.Log.Information("Done seeding database. Exiting.");
        return;
    }

    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Serilog.Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Serilog.Log.Information("Shut down complete");
    Serilog.Log.CloseAndFlush();
}