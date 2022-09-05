using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SqlClient;
using VizORM_Backend.DTO;

namespace VizORM_Backend
{
    public class Query : IQueryExecutable
    {
        private QueryFactory _queryFactory;

        public Query(SqlConnection sqlConnection, Compiler compiler)
        {
            _queryFactory = new QueryFactory(sqlConnection, compiler);
        }

        public Task<int> DeleteAsync(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<dynamic>> SelectAsync(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }
    }
}
