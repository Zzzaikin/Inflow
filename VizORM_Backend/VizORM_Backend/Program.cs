using Microsoft.AspNetCore.Localization;
using System.Globalization;
using VizORM_Backend.Config;
using VizORM_Backend.Middlewares;

namespace VizORM_Backend
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

            builder.Services.Configure<Configuration>(builder.Configuration);
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var supportedCultures = new[]
            {
                new CultureInfo("ru"),
                new CultureInfo("en")
            };

            var cultureName = builder.Configuration["Culture"];
            app.UseRequestLocalization(new RequestLocalizationOptions 
            {
                DefaultRequestCulture = new RequestCulture(cultureName),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();

            app.UseMiddleware<DataExceptionHandler>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}