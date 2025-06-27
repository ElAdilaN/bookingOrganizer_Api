using bookingOrganizer_Api.DAO;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.Models;
using bookingOrganizer_Api.Repository;
using bookingOrganizer_Api.Service;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API_SAGE_ESCIO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Register DbContext using connection string from appsettings.json
            builder.Services.AddDbContext<BookingContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ✅ Register dependencies
            builder.Services.AddScoped<IDAOBookingInfo, DAOBookingInfo>();
            builder.Services.AddScoped<RepoBookingInfo, ServiceBookingInfo>();

            builder.Services.AddScoped<IDAOTypeBooking, DAOTypeBooking>();
            builder.Services.AddScoped<RepoTypeBooking, ServiceTypeBooking>();

            builder.Services.AddScoped<IDAOGroup, DAOGroup>();
            builder.Services.AddScoped<RepoGroup, ServiceGroup>();

            // ✅ Add services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ✅ Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            var app = builder.Build();

            // ✅ Middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
