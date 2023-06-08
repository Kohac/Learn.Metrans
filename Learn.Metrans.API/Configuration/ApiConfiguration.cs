using Learn.Metrans.API.Services;
using Learn.Metrans.CORE.Utilities;
using Learn.Metrans.PERSISTANCE.Context;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Learn.Metrans.API.Configuration;
/// <summary>
/// Application configuration
/// </summary>
public static class ApiConfiguration
{
    private static readonly string _apiName = "MetransEmployyes";
    /// <summary>
    /// Configure dependency injections on service collection
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc(_apiName, new OpenApiInfo
            {
                Title = "Rest API for Metrans Employees",
                Description = "CRUD API to handle employyes for Employees",
                Version = "v1"
            });
            var fileFullPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            o.IncludeXmlComments(fileFullPath);
        });

        services.Configure<JsonOptions>(o => o.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

        services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        services.AddDbContext<MetransDbContext>(o =>
        {
            o.UseInMemoryDatabase("MetransHR_CZ");
        }, ServiceLifetime.Singleton);
        return services;
    }
    /// <summary>
    /// Configure web application
    /// </summary>
    /// <param name="app"><see cref="WebApplication"/></param>
    /// <param name="config">Instance of Iconfiguration</param>
    /// <returns><see cref="WebApplication"/></returns>

    public static WebApplication ConfigureWebApplication(this WebApplication app, IConfiguration config)
    {
        app.UseSwagger();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint($"/swagger/{_apiName}/swagger.json", _apiName);
                o.RoutePrefix = string.Empty;
            });
        }
        //Swagger configuration to run on IIS
        else
        {
            app.UseSwaggerUI(o =>
            {
                var iisPath = config.GetRequiredSection("swaggerConfiguration").GetValue<string>("iisPath");
                o.SwaggerEndpoint($"{iisPath}/swagger/{_apiName}/swagger.json", _apiName);
                o.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();

        return app;
    }
    /// <summary>
    /// Insert employyes on initialization
    /// </summary>
    /// <param name="app"><see cref="WebApplication"/></param>
    /// <returns><see cref="WebApplication"/></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static WebApplication InsertEmployeesOnInitialization(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<MetransDbContext>();
            context.Employyes.AddRange(Utils.CreateInitEmployees(100));
            context.SaveChanges();
        }
        catch
        {
            throw new NotImplementedException();
        }
        return app;
    }
}
