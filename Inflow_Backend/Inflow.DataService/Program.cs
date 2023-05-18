using Microsoft.AspNetCore.Localization;
using Inflow.DataService.Middlewares;
using Inflow.DataService.Extensions;

namespace Inflow.DataService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<Configuration>(builder.Configuration);
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            var sqlOptionsName = builder.Configuration.GetValue<string>("SqlOptionsName");
            builder.Services.AddSingletonSqlOptions(sqlOptionsName);
            builder.Services.AddSingletonSqlSchema(sqlOptionsName);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var configuration = app.Configuration.Get<Configuration>();
            var cultureName = configuration.Culture;
            var supportedCultures = configuration.SupportedCultures.ToList();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(cultureName),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();
            app.UseMiddleware<ExceptionHandler>();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}