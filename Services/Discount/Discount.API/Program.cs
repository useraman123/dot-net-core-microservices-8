using Discount.API.ExceptionMiddleware;
using Discount.API.Services;
using Discount.Application.Handler;
using Discount.Core.Repositories;
using Discount.Infrastructure.Extension;
using Discount.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//Register Mediator
var assembly = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(CreateDiscountHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));


//Register Application Services
builder.Services.AddScoped<IDiscount, DiscountRepository>();

//Add Grop
builder.Services.AddGrpc();
var app = builder.Build();

//migrate Database

app.MigrateDatabase<Program>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.UseAuthorization();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communitation with GRPC server must be using GRPC Client");
    });
});

app.Run();