namespace Todo.API.Filter
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Net;

    public class IPAddressResourceFilter : IResourceFilter
    {
        private readonly List<string> _allowedIPAddresses;

        public IPAddressResourceFilter(IEnumerable<string> allowedIPAddress)
        {
            _allowedIPAddresses = allowedIPAddress.ToList();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var requestIPAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            if (!_allowedIPAddresses.Contains(requestIPAddress))
            {
                // If the request is from an unauthorized IP address, short-circuit and return 403 Forbidden
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // This method is called after the rest of the pipeline has executed, including the action method.
            // You can add any additional logic here if needed.
        }
    }

}
