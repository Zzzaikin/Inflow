using Inflow.Data.Options;

namespace Inflow.Data.Schema
{
    public class SqlServerSchema : BaseQuery, ISchema
    {
        public SqlServerSchema(BaseSqlOptions sqlOptions) : base(sqlOptions) { }

        public Task GetAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
