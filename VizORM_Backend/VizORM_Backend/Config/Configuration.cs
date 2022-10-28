using Microsoft.Extensions.Localization;
using SqlKata.Compilers;
using VizORM.Common;

namespace VizORM.DataService.Config
{
    public class Configuration
    {
        private readonly IStringLocalizer<Configuration> _stringLocalizer;

        public ConnectionStrings ConnectionStrings { get; set; }

        public int MaxSelectedRecords { get; set; }

        public string SqlCompilerName { get; set; }

        public string Culture { get; set; }

        public Configuration(IStringLocalizer<Configuration> stringLocalizer) => _stringLocalizer = stringLocalizer;

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
                    var stringFromResource = _stringLocalizer["SqlCompilerNotImplemented"];
                    var errorMessage = string.Format(stringFromResource, nameof(sqlCompilerName));

                    throw new NotImplementedException(errorMessage);
            }
        }
    }
}
