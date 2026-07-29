using BlazorCodeChallenge.Core.Diagnostics;
using BlazorCodeChallenge.Core.Engineering;
using BlazorCodeChallenge.EnterpriseApi.Infrastructure.Health;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHealthChecks();

//builder.Services.AddProblemDetails();

builder.Services.AddScoped<IEngineeringSuiteService, EngineeringSuiteService>();
builder.Services.AddScoped<IRuntimeDiagnosticsService, RuntimeDiagnosticsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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