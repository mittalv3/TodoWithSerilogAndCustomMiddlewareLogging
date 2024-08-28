namespace Todo.API.Filter
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;

    public class AdminOnlyAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the request contains the specific header
            var userRole = context.HttpContext.Request.Headers["X-User-Role"].ToString();

            // If the user role is not Admin, deny access
            if (!string.Equals(userRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                // Return a 403 Forbidden response if the user is not authorized
                context.Result = new ForbidResult();
            }
        }
    }

}
