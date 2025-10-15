using Microsoft.AspNetCore.Diagnostics;
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

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

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
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var error = exceptionHandlerPathFeature?.Error;

            logger.LogError(error, "Unhandled exception occurred during request.");

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var result = new
            {
                StatusCode = 500,
                Message = "An unexpected error occurred. Please try again later.",
                Detail = builder.Environment.IsDevelopment() ? error?.Message : null
            };

            await context.Response.WriteAsJsonAsync(result);
        });
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // HTTPS redirect
    app.UseHttpsRedirection();

    // Authorization (JWT later when reuired  tu)
    app.UseAuthorization();

    // Map routes to controllers
    app.MapControllers();
    logger.LogInformation("Shortener Api is up and running ??");
    app.Run();
}
catch (Exception ex)
{
    logger.LogCritical(ex, "Shortener Api failed to start due to a fatal error.");
    throw;
}
finally
{
    logger.LogInformation("Shirtener Api shutdown sequence complete.");
}
