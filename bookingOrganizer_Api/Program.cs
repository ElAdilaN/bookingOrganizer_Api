using bookingOrganizer_Api.DAO;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.Models;
using bookingOrganizer_Api.Repository;
using bookingOrganizer_Api.Service;
using bookingOrganizer_Api.UTILS;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
            builder.Services.AddSwaggerGen(

                options =>
                {
                    var jwtSecurityScheme = new OpenApiSecurityScheme
                    {
                        BearerFormat = "JWT",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        Description = "Enter your JWT Access Token",
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    };
                    options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {jwtSecurityScheme , Array.Empty<string>() }
                    });

                }
                );
            builder.Services.AddAutoMapper(typeof(Program));


            builder.Services.AddScoped<JwtService>();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
                        ValidAudience = builder.Configuration["JwtConfig:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });
            builder.Services.AddAuthorization();


            // ✅ Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

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
