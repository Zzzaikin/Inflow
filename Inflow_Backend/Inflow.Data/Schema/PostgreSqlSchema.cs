using Inflow.Data.Options;

namespace Inflow.Data.Schema
{
    public class PostgreSqlSchema : BaseQuery, ISchema
    {
        public PostgreSqlSchema(BaseSqlOptions sqlOptions) : base(sqlOptions) { }

        public Task GetAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
