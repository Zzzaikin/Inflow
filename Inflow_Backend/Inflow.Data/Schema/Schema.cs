using SqlKata.Execution;
using Inflow.Common;
using Inflow.Data.Models;

namespace Inflow.Data.Schema
{
    public class Schema : BaseQuery, ISchema
    {

        public Schema(QueryFactory databaseProvider) : base(databaseProvider) { }

        public async Task GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SectionsDisplayedInNav>> GetSectionsDisplayedInNavAsync(int limit, int offset)
        {
            Argument.IsNotLessThanZero(limit, nameof(limit));
            Argument.IsNotLessThanZero(offset, nameof(offset));

            return await DatabaseProvider
                .Query("SectionsDisplayedInNav")
                .Select("TableName", "Image")
                .Limit(limit)
                .Offset(offset)
                .GetAsync<SectionsDisplayedInNav>();
        }
    }
}
