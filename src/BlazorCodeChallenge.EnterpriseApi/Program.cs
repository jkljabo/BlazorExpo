using BlazorCodeChallenge.Core.Diagnostics;
using BlazorCodeChallenge.Core.Engineering;
using BlazorCodeChallenge.Core.Mortgage;
using BlazorCodeChallenge.EnterpriseApi.Infrastructure.Health;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHealthChecks();

builder.Services.AddProblemDetails();

builder.Services.AddScoped<IEngineeringSuiteService, EngineeringSuiteService>();
builder.Services.AddScoped<IRuntimeDiagnosticsService, RuntimeDiagnosticsService>();
builder.Services.AddScoped<IMortgageCalculatorService, MortgageCalculatorService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Blazor Code Challenge Engineering API",
        Version = "v1",
        Description =
            "REST API providing engineering diagnostics and mortgage calculation services.",
        Contact = new OpenApiContact
        {
            Name = "Jason Little",
            Email = "jason.k.little@comcast.net",
            Url = new Uri("https://www.linkedin.com/in/jason-little-4623a1198")
        }
    });

    var xmlFilename =
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath =
        Path.Combine(AppContext.BaseDirectory, xmlFilename);

    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Blazor Code Challenge API";

        options.DefaultModelsExpandDepth(-1);

        options.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks(
    "/health",
    new HealthCheckOptions
    {
        ResponseWriter = HealthCheckResponseWriter.WriteResponse
    });

app.Run();