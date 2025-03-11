namespace Multi_Level_Blogging_System.Middleware;

public class UnauthorizedMiddleware
{
    private readonly RequestDelegate _next;

    public UnauthorizedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                message = "Unauthorized access",
                statusCode = 401
            });
        }
        else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                message = "Access forbidden",
                statusCode = 403
            });
        }
    }
}
