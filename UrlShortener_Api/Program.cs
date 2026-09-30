using Microsoft.AspNetCore.Diagnostics;
using Scalar.AspNetCore;
using UrlShortener_Infrastructure.Infrastructure_Commons;


var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var loggerFactory = LoggerFactory.Create(logging =>
{
    logging.AddConsole();
});

var logger = loggerFactory.CreateLogger("Program");
logger.LogInformation("Shortener API is starting...");
logger.LogInformation("Running in {Environment}", builder.Environment.EnvironmentName);

try
{
    builder.Services.AddControllers();

    // OpenAPI
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddOpenApi();
    builder.Services.AddContextDI(
        builder.Configuration,
        builder.Environment,
        logger
    );

    var app = builder.Build();

    app.UseExceptionHandler(exceptionApp =>
    {
        exceptionApp.Run(async context =>
        {
            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            var error = exceptionHandlerPathFeature?.Error;

            logger.LogError(
                error,
                "Unhandled exception occurred during request."
            );

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var result = new
            {
                StatusCode = 500,
                Message = "An unexpected error occurred. Please try again later.",
                Detail = builder.Environment.IsDevelopment()
                    ? error?.Message
                    : null
            };

            await context.Response.WriteAsJsonAsync(result);
        });
    });

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    logger.LogInformation("Shortener API is up and running");
    app.Run();
}
catch (Exception ex)
{
    logger.LogCritical(
        ex,
        "Shortener API failed to start due to a fatal error."
    );

    throw;
}
finally
{
    logger.LogInformation("Shortener API shutdown sequence complete.");
}
