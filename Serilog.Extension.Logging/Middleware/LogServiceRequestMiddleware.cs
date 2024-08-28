using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Serilog.Extension.Logging.Middleware
{
    public class LogServiceRequestMiddleware
    {
        private readonly RequestDelegate _next;


        public LogServiceRequestMiddleware(RequestDelegate next) 
        {
            _next = next;
        }
    

        public async Task InvokeAsync(HttpContext context) {
            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

            // Call the next middleware in the pipeline
            await _next(context);

            // Custom logic after the next middleware has processed the request
            Console.WriteLine($"Response: {context.Response.StatusCode}");
        }
    }
}
