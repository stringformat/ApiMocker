using ApiMocker;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var apiEndPointsProvider = new ApiEndPointsProvider(app.Configuration);
var endPoints = await apiEndPointsProvider.GetEndPoints();

foreach (var endPoint in endPoints)
{
    app.Map(endPoint.Url, async context =>
    {
        context.Response.StatusCode = (int)endPoint.ResponseCode;

        if (endPoint.ResponseHeaders != null)
        {
            foreach (var (key, value) in endPoint.ResponseHeaders)
            {
                context.Response.Headers.Add(key, new StringValues(value));
            }
        }

        if (endPoint.ResponseContentType != null) 
            context.Response.ContentType = endPoint.ResponseContentType;
        
        if (endPoint.ResponsePath != null)
        {
            var file = File.ReadAllBytes(endPoint.ResponsePath);
            await context.Response.Body.WriteAsync(file);
        }
    });

    app.Logger.LogInformation("API endpoint added: {Url} {ResponseCode} {ResponseContentType}", 
        endPoint.Url, 
        (int)endPoint.ResponseCode,
        endPoint.ResponseContentType ?? string.Empty);
}

app.Run();