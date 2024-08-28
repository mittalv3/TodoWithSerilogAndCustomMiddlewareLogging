using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Reflection;
using Todo.API.DbContexts;
using Todo.API.Repository;
using Serilog.Extension.Logging;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(
            outputTemplate:
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} <s:{SourceContext}> {Exception} {NewLine}")
     .MinimumLevel.Debug()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    //This will redirect all log events through your serilog pipeline.
    builder.Host.UseSerilog((context, loggerConfiguration) =>
        loggerConfiguration.ReadFrom.Configuration(context.Configuration));

    // Add services to the container.

    builder.Services.AddProblemDetails();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    // Database configuration setup

    // Use SQLite as DB
    /*builder.Services.AddDbContext<ToDoDbContext>(dbContextOptions
        => dbContextOptions.UseSqlite(
            builder.Configuration["ConnectionStrings:ToDosDBConnectionString"]));*/

    builder.Services.AddDbContext<ToDoDbContext>(dbContextOptions
        => dbContextOptions.UseSqlServer(
            builder.Configuration["ConnectionStrings:ToDosDBConnectionString_SQLSERVER"]));

    // Register the Repository 
    builder.Services.AddScoped<IToDoRepository, ToDoRepository>();

    // Register the AutoMapper in the container
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Register Authentication related services on the container
    builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Authetication:Issuer"],
                ValidAudience = builder.Configuration["Authetication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Convert.FromBase64String(builder.Configuration["Authetication:SecretForKey"]))
            };
        });

    builder.Services.AddSwaggerGen(setupAction =>
    {
        var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

        setupAction.IncludeXmlComments(xmlCommentsFullPath);

        setupAction.AddSecurityDefinition("ToDoAPIBearerAuth", new()
        {
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "Bearer",
            Description = "Input a valid token to access the API"
        });

        setupAction.AddSecurityRequirement(new()
        {
            {
                new()
                {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ToDoAPIBearerAuth" }
                },
                new List<string>()
            }
        });
    });


    var app = builder.Build();

    // Configure the HTTP request pipeline.

    // Ensure the database is created
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
        dbContext.Database.EnsureCreated();
    }

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler();
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    //Add Custom Middleware

    app.UseSerilogLoggingMiddleware();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
