
using Microsoft.AspNetCore.Builder;
using Serilog.Extension.Logging.Middleware;

namespace Serilog.Extension.Logging
{
    public static class SerilogLoggingMiddleware
    {
        public static IApplicationBuilder UseSerilogLoggingMiddleware(this IApplicationBuilder app) 
        {
            return app.UseMiddleware<LogServiceRequestMiddleware>();
        }

    }
}
