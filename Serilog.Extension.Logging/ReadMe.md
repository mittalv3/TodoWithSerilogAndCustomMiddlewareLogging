This project is used for understanding and do some practice on Custom Middleware for Serilog Logging.

## Steps to create Custom Middleware for Serilog Logging.
#  Follow https://mittalv3.atlassian.net/wiki/spaces/Dev/pages/772341761/How+to+create+a+general+custom+middleware
1. Create a new ASP.NET Core Web Application.
2. Create a class library project for Custom Middleware.
   --> for ex: Serilog.Extension.Logging
3. Add reference of Serilog.Extension.Logging project to the main project.

4. In the Serilog.Extension.Logging project, create a middleware class that handles the logging logic.
   --> for ex: LogServiceRequestMiddleware.cs
5. Create an extension method to add the middleware to the application pipeline.
   --> for ex: SerilogLoggingMiddleware.cs
6. Add the middleware to the application pipeline in the Configure method of the program class.
   --> for ex:     app.UseSerilogLoggingMiddleware();

