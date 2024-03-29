
using FindTheDwarves.Application;
using FindTheDwarves.Application.Interface;
using FindTheDwarves.Application.Service;
using FindTheDwarves.Domain.Interface;
using FindTheDwarves.Domain.Model;
using FindTheDwarves.Infrastructure;
using FindTheDwarves.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FindTheDwarvesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var authenticationSettings = new AuthenticationSettings();

            builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddDbContext<Context>(
                opt => opt.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                    )
                );

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme= "Bearer";
                opt.DefaultScheme = "Bearer";
                opt.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata= false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = "http://FindTheDwarfAPI.pl",
                    ValidAudience = "http://FindTheDwarfAPI.pl",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PSIM_Project"))
                };
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<IAchievementRepository, AchievementRepository>();
            builder.Services.AddTransient<IDwarfRepository, DwarfRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            builder.Services.AddTransient<IAchievementService, AchievementService>();
            builder.Services.AddTransient<IDwarfService, DwarfService>();
            builder.Services.AddTransient<IUserService, UserService>();

            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            builder.Services.AddSingleton(authenticationSettings);
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}