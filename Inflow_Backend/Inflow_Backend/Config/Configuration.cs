using Inflow.Common;
using Inflow.Common.Database;

namespace Inflow.DataService.Config
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
