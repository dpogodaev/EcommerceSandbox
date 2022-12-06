using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace EcommerceSandbox.WebMvc.Configs.Common;

/// <summary>
/// Exception handling configuration.
/// </summary>
public static class ExceptionHandlerConfig
{
    /// <summary>
    /// Adds custom exception handling that produces a <see cref="ProblemDetails"/> response for all unhandled exceptions.
    /// </summary>
    /// <param name="app">Provides the mechanisms to configure an application's request pipeline.</param>
    /// <param name="env">Provides information about the hosting environment an application is running in.</param>
    /// <remarks>
    /// The <see cref="ProblemDetails.Status"/> is always set to 500 (Internal server error).<br/>
    /// The <see cref="ProblemDetails.Title"/> contains the text "An error occured" and an exception message (for the development environment only).<br/>
    /// The <see cref="ProblemDetails.Detail"/> contains an exception and provided only for the development environment.<br/>
    /// The <see cref="ProblemDetails.Extensions"/> contains the trace ID.
    /// </remarks>
    public static void UseCustomExceptionHandler(this IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.Use(WriteDevelopmentResponse);
        }
        else
        {
            app.Use(WriteProductionResponse);
        }
    }

    #region Private methods

    private static Task WriteDevelopmentResponse(HttpContext context, Func<Task> next)
        => WriteResponse(context, withDetails: true);

    private static Task WriteProductionResponse(HttpContext context, Func<Task> next)
        => WriteResponse(context, withDetails: false);

    private static async Task WriteResponse(HttpContext context, bool withDetails)
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception != null)
        {
            context.Response.ContentType = "application/problem+json";

            await JsonSerializer.SerializeAsync(context.Response.Body, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = withDetails ? $"An error occured: {exception.Message}" : "An error occured",
                Detail = withDetails ? exception.ToString() : null,
                Extensions = { ["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier }
            });
        }
    }

    #endregion
}