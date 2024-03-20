using AutoMapper;
using CMS_API.Mappers;
using CMS_API.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace CMS_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Auto Mapper
            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperConfiguration()); });
            var mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddDbContext<PROJECT_PRN231Context>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
                       
            //Authen
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme; //Cookies
                option.DefaultChallengeScheme= CookieAuthenticationDefaults.AuthenticationScheme; // Cookies
            }).AddCookie(options =>
            {
                options.Cookie = new CookieBuilder()
                {
                    Name = "sieuphameCookie",
                    //Domain = "google.com"
                };

                options.LoginPath = "/api/authen/unauthorized"; // Điều hướng tới trang login nếu chưa đăng nhập
                options.LogoutPath = "/api/authen/logout";
                options.AccessDeniedPath = "/api/authen/forbindden";

            });

            var app = builder.Build();

            app.UseCors
                (policy =>
                policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}