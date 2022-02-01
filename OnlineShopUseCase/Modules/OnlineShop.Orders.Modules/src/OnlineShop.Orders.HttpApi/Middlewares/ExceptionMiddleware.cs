using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace TheCompany.HttpApi.Common.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                { 
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    if (contextFeature != null)
                    {
                        var ex = contextFeature?.Error;
                        var isDev = env.IsDevelopment();

                        await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
                        {
                            Type = ex.GetType().Name,
                            Status = (int)HttpStatusCode.InternalServerError,
                            Instance = contextFeature?.Path,
                            Title = isDev ? $"{ex.Message}" : "An error occurred.",
                            Detail = isDev ? ex.StackTrace : null
                        })).ConfigureAwait(false);
                    }
                });
            });
        }
        private sealed class ProblemDetails
        {
            public string Type { get; internal set; }
            public int Status { get; internal set; }
            public string Instance { get; internal set; }
            public string Title { get; internal set; }
            public string Detail { get; internal set; }
        }
    }
}
