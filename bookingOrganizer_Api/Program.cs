using bookingOrganizer_Api.DAO;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.Models;
using bookingOrganizer_Api.Repository;
using bookingOrganizer_Api.Service;
using Microsoft.EntityFrameworkCore;

namespace API_SAGE_ESCIO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext using connection string from appsettings.json
            builder.Services.AddDbContext<BookingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register the repository interface and implementation
            builder.Services.AddScoped<IDAOBookingInfo,DAOBookingInfo>();
            builder.Services.AddScoped<RepoBookingInfo,ServiceBookingInfo>();

            // Add services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            var app = builder.Build();

            // Enable Swagger in all environments
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            { 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty; // Swagger at root URL
            });

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
