using System;

namespace TimeTracking.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            context.Items["CustomData"] = "This is some custom data";
            // Custom logic before the next middleware
            await _next(context);
            // You can access the custom data later in the pipeline
            var customData = context.Items["CustomData"] as string;
            if (customData != null)
            {
                Console.WriteLine($"Custom Data: {customData}");
            }
            System.Console.WriteLine(context.User.Identity?.Name ?? "No user identity");
            // Custom logic after the next middleware
        }
    }
}
