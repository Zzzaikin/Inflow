using Microsoft.Extensions.Localization;
using SqlKata.Compilers;
using VizORM.Common;
using VizORM.Common.Exceptions;

namespace VizORM.DataService.Config
{
    public class Configuration
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public int MaxSelectedRecords { get; set; }

        public string SqlCompilerName { get; set; }

        public string Culture { get; set; }

        public Compiler GetDbCompiler(string sqlCompilerName)
        {
            Argument.NotNullOrEmpty(sqlCompilerName, nameof(sqlCompilerName));
    
            switch (sqlCompilerName)
            {
                case nameof(SqlServerCompiler):
                    return new SqlServerCompiler();

                case nameof(MySqlCompiler):
                    return new MySqlCompiler();

                case nameof(FirebirdCompiler):
                    return new FirebirdCompiler();

                case nameof(OracleCompiler):
                    return new OracleCompiler();

                case nameof(PostgresCompiler):
                    return new PostgresCompiler();

                case nameof(SqliteCompiler):
                    return new SqliteCompiler();

                default:
                    throw new SqlCompilerNotImplementedException();
            }
        }
    }
}
