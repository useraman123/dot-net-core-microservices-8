using Asp.Versioning;
using Catalog.API.ExceptionMiddleware;
using Catalog.Application.Handler;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using Common.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//serilog configuration
builder.Host.UseSerilog(Logging.ConfigureLogger);

// Add services to the container.
builder.Services.AddControllers();
//API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//adding meta data into swagger
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Catalog API", Version = "v1" }); });

#region Services
//Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//Register Mediator
var assembly = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetAllBrandsHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
//Register Application Services
builder.Services.AddScoped<ICatalogContext,CatalogContext>();
builder.Services.AddScoped<ITypesRepository, ProductRepository>();
builder.Services.AddScoped<IBrandRepository, ProductRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


//Identity Server Changes
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:44386";
        options.Audience = "catalog";
    });
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
