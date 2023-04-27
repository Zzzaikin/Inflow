using Inflow.Data.Options;

namespace Inflow.Data.Schema
{
    public class MySqlSchema : BaseQuery, ISchema
    {
        public MySqlSchema(BaseSqlOptions sqlOptions) : base(sqlOptions) { }

        public Task GetAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
