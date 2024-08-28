namespace Todo.API.Filter
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.LogError(context.Exception, "An unhandled exception occurred during the request.");

            // Create a custom error response
            var errorResponse = new
            {
                Message = "An error occurred while processing your request.",
                Detail = context.Exception.Message // You might want to exclude the detail in a production environment
            };

            // Set the result to a JSON response with a 500 status code
            context.Result = new JsonResult(errorResponse)
            {
                StatusCode = 500
            };

            // Indicate that the exception has been handled
            context.ExceptionHandled = true;
        }
    }

}
