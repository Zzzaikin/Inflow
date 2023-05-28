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
            var services = builder.Services;

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.Configure<Configuration>(builder.Configuration);
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            var builderConfiguration = builder.Configuration;
            var sqlOptionsName = builderConfiguration.GetValue<string>("SqlOptionsName");
            var dbConnectionString = builderConfiguration.GetConnectionString("DbConnectionString");
            
            services.AddSingletonSqlOptions(sqlOptionsName, dbConnectionString);
            services.AddSingletonDatabaseProvider();
            services.AddSingletonInflowDataQuery();
            services.AddSingletonSqlSchema();
            

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var appConfiguration = app.Configuration.Get<Configuration>();
            var cultureName = appConfiguration.Culture;
            var supportedCultures = appConfiguration.SupportedCultures.ToList();

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