using Inflow.Data.Options;
using SqlKata.Execution;

namespace Inflow.Data
{
    public abstract class BaseQuery
    {
        public QueryFactory Database { get; private set; }

        protected BaseQuery(BaseSqlOptions sqlOptions)
        {
            Database = new QueryFactory(sqlOptions.DbConnection, sqlOptions.Compiler);
        }
    }
}
