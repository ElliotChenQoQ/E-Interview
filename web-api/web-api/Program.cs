using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebApp.Extensions;
using WebApp.Filters;
using WebApp.Middlewares;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.Services;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<LogActionFilter>();
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCustomSwagger();
            builder.Services.AddCustomCors();
            // MediatR 
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());  
            // AutoMapper 
            builder.Services.AddAutoMapper(typeof(Program));
            // Register Repositories, Services, etc.
            builder.Services.AddCustomDbContext(builder.Configuration);
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            // Error Handling 
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.MapControllers();
            app.Run();
        }
    }
}
