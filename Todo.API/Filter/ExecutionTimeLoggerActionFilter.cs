namespace Todo.API.Filter
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;

    public class ExecutionTimeLoggerActionFilter : IActionFilter
    {
        private readonly ILogger<ExecutionTimeLoggerActionFilter> _logger;
        private Stopwatch _stopwatch;

        public ExecutionTimeLoggerActionFilter(ILogger<ExecutionTimeLoggerActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Start the stopwatch before the action executes
            _stopwatch = Stopwatch.StartNew();
            _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} is starting.");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Stop the stopwatch after the action has executed
            _stopwatch.Stop();
            _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} " +
                $"executed in {_stopwatch.ElapsedMilliseconds} ms.");
        }
    }

}
