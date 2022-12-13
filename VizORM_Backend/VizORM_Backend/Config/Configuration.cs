using VizORM.Common;
using VizORM.Common.Database;

namespace VizORM.DataService.Config
{
    public class Configuration
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public int MaxSelectedRecords { get; set; }

        public string SqlCompilerName { get; set; }

        public string Culture { get; set; }

        public Options DbOptions => ConfigurationUtilities.GetDbOptions(SqlCompilerName, ConnectionStrings.DbConnectionString);
    }
}
