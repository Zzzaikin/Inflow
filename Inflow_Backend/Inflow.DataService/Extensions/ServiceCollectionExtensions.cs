using Inflow.Common;
using Inflow.Data.Options;
using Inflow.Data.Schema;

namespace Inflow.DataService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSingletonSqlOptions(this IServiceCollection serviceCollection,
            string sqlOptionsName)
        {
            Argument.IsNotNullOrEmpty(sqlOptionsName, nameof(sqlOptionsName));

            switch (sqlOptionsName) 
            {
                case nameof(SqlServerOptions):
                    return serviceCollection.AddSingleton<BaseSqlOptions, SqlServerOptions>();

                case nameof(PostgreSqlOptions):
                    return serviceCollection.AddSingleton<BaseSqlOptions, PostgreSqlOptions>();

                case nameof(MySqlOptions):
                    return serviceCollection.AddSingleton<BaseSqlOptions, MySqlOptions>();

                default:
                    var exceptionMessage = string.Format(Resources.SqlOptionsAreNotImplemented, sqlOptionsName);
                    throw new NotImplementedException(exceptionMessage);
            }
        }

        public static IServiceCollection AddSingletonSqlSchema(this IServiceCollection serviceCollection,
            string sqlOptionsName)
        {
            Argument.IsNotNullOrEmpty(sqlOptionsName, nameof(sqlOptionsName));

            switch (sqlOptionsName)
            {
                case nameof(SqlServerOptions):
                    return serviceCollection.AddSingleton<ISchema>(serviceProvider => 
                    {
                        return new SqlServerSchema(serviceProvider.GetRequiredService<SqlServerOptions>());
                    });

                case nameof(PostgreSqlOptions):
                    return serviceCollection.AddSingleton<ISchema>(serviceProvider => 
                    {
                        return new PostgreSqlSchema(serviceProvider.GetRequiredService<PostgreSqlOptions>());
                    });

                case nameof(MySqlOptions):
                    return serviceCollection.AddSingleton<ISchema>(serviceProvider => 
                    {
                        return new MySqlSchema(serviceProvider.GetRequiredService<MySqlOptions>());
                    });

                default:
                    var exceptionMessage = string.Format(Resources.SqlSchemaIsNotImplementedForSqlOptions);
                    throw new NotImplementedException(exceptionMessage);
            }
        }
    }
}
