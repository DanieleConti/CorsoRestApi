using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.Middlewares;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class MyMiddleware
{
    private readonly RequestDelegate _next;

    public MyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        await httpContext.Response.WriteAsync($"Hello from MY MW {Environment.NewLine}");

        await _next(httpContext);

        await httpContext.Response.WriteAsync($"GoodBye from MY MW {Environment.NewLine}");
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class MyMiddlewareExtensions
{
    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MyMiddleware>();
    }
}
