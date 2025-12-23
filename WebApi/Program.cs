using Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Persistence.Extension;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using WebApi.CustomMiddleware.NLog;
using WebApi.CustomMiddlewares.Claims;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
             .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddNLogWeb();
        });

        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", builder.Configuration.GetConnectionString("DefaultConnection"));

        builder.Services.AddPersistenceLayer(builder.Configuration, builder.Environment);

        builder.Services.ApplicationLayer();
        builder.Services.AddInfrastructure();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 60, // Max 60 requests
                        Window = TimeSpan.FromMinutes(1) // Per 1 minute
                    }));

            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = 429;
                await context.HttpContext.Response.WriteAsync("Too many requests! Try again after 1 minute");
            };
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "AllowOrigin",
                builder =>
                {
                    builder.AllowAnyMethod()    // Allows any HTTP methods
                           .AllowAnyHeader()    // Allows any headers
                                                //.SetIsOriginAllowed(host => true)
                           .SetIsOriginAllowed(_ => true)// Allows any origin (for dev, be more specific in prod)
                           .AllowCredentials(); // Allows credentials like cookies or tokens
                });
        });

        // Configure Swagger
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });

            // Optional: Custom operation IDs
            c.CustomOperationIds(apiDesc =>
            {
                return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
            });

            // Add JWT Bearer Security
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter 'Bearer {Token}'",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            c.AddSecurityDefinition("Bearer", securityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    Array.Empty<string>()
                },
            });
        });

        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 30 * 1024 * 1024; // setting max upload file size to 30MB
        });

        var app = builder.Build();

        // Middleware configuration
        app.UseHttpsRedirection();
        app.UseHsts();

        app.UseCors("AllowOrigin");

        app.UseRateLimiter();

        app.UseMiddleware<NLogMiddleware>();

        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        //app.UseMiddleware<ClaimMiddleware>();

        // Enable Swagger in all environments
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("./swagger/v1/swagger.json", "Web Api v1");
                options.RoutePrefix = "";
            });
        }

        app.MapControllers();

        app.Run();
    }
}
