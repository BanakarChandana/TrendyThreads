using Microsoft.OpenApi.Models;

namespace ThrendyThreads
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Add Controller support
            builder.Services.AddControllers();

            // Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS configuration for React
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReact", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Enable CORS
            app.UseCors("AllowReact");

            app.UseAuthorization();

            // Map Controllers
            app.MapControllers();

            app.Run();
        }
    }
}