using System.Data.Common;
using System.Data.SqlClient;
using Npgsql;
using MySql.Data.MySqlClient;
using SqlKata.Compilers;
using Inflow.Common;

namespace Inflow.DataService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSingetonSqlCompiler(this IServiceCollection services, 
            string sqlCompilerName) 
        {
            Argument.IsNotNullOrEmpty(sqlCompilerName, nameof(sqlCompilerName));

            switch (sqlCompilerName)
            {
                case nameof(SqlServerCompiler):
                    return services.AddSingleton<Compiler, SqlServerCompiler>();

                case nameof(MySqlCompiler):
                    return services.AddSingleton<Compiler, MySqlCompiler>();

                case nameof(PostgresCompiler):
                    return services.AddSingleton<Compiler, PostgresCompiler>();

                default:
                    var exeptionMessage = string.Format(Resources.SqlCompilerNotImplemented, sqlCompilerName);
                    throw new NotImplementedException(exeptionMessage);
            }
        }

        public static IServiceCollection AddSingletonSqlConnection(this IServiceCollection services, 
            string sqlCompilerName)
        {
            Argument.IsNotNullOrEmpty(sqlCompilerName, nameof(sqlCompilerName));

            switch (sqlCompilerName)
            {
                case nameof(SqlServerCompiler):
                    return services.AddSingleton<DbConnection, SqlConnection>();

                case nameof(MySqlCompiler):
                    return services.AddSingleton<DbConnection, MySqlConnection>();

                case nameof(PostgresCompiler):
                    return services.AddSingleton<DbConnection, NpgsqlConnection>();

                default:
                    var exeptionMessage = string.Format(Resources.SqlConnectionForThisSqlCompilerNotImplemented,
                        sqlCompilerName);

                    throw new NotImplementedException(exeptionMessage);
            }
        }
    }
}
