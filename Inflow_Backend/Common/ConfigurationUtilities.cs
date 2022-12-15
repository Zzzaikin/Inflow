using SqlKata.Compilers;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using InflowNotImplementedException = Inflow.Common.Exceptions.NotImplementedException;
using Npgsql;

namespace Inflow.Common
{
    //
    public static class ConfigurationUtilities
    {
        public static Database.Options GetDbOptions(string sqlCompilerName, string connectionString)
        {
            Argument.NotNullOrEmpty(sqlCompilerName, nameof(sqlCompilerName));

            switch (sqlCompilerName)
            {
                case nameof(SqlServerCompiler):
                    return new Database.Options 
                    {
                        SqlCompiler = new SqlServerCompiler(),
                        SqlConnection = new SqlConnection(connectionString)
                    };

                case nameof(MySqlCompiler):
                    return new Database.Options
                    {
                        SqlCompiler = new MySqlCompiler(),
                        SqlConnection = new MySqlConnection(connectionString)
                    };

                case nameof(PostgresCompiler):
                    return new Database.Options
                    {
                        SqlCompiler = new PostgresCompiler(),
                        SqlConnection = new NpgsqlConnection(connectionString)
                    };

                default:
                    var exeptionMessage = $"Sql comliler such as {sqlCompilerName} not implemented";
                    throw new InflowNotImplementedException(sqlCompilerName, "SqlCompilerNotImplemented", exeptionMessage);
            }
        }
    }
}
