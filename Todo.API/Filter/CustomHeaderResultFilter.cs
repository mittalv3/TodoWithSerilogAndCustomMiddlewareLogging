namespace Todo.API.Filter
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    public class CustomHeaderResultFilter : IResultFilter
    {
        private readonly ILogger<CustomHeaderResultFilter> _logger;

        public CustomHeaderResultFilter(ILogger<CustomHeaderResultFilter> logger)
        {
            _logger = logger;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            // Add a custom header before the result is executed
            context.HttpContext.Response.Headers.Add("X-Custom-Header", "This is a custom header");

            // Log the type of result
            _logger.LogInformation($"Result type: {context.Result.GetType().Name}");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // You can perform additional logic after the result has been executed
            _logger.LogInformation("Result has been executed.");
        }
    }

}
