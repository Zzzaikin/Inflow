using System.Data.Common;
using SqlKata.Compilers;
using SqlKata.Execution;
using Inflow.Common;
using Inflow.Data.DTO;
using Inflow.Data.Extensions;

namespace Inflow.Data
{
    public class Query : IQueriable
    {
        private readonly Compiler _sqlCompiler;

        private readonly DbConnection _dbConnection;

        private readonly QueryFactory _database;

        public Query(Compiler compiler, DbConnection connection)
        {
            Argument.NotNull(compiler, nameof(compiler));
            Argument.NotNull(connection, nameof(connection));

            _sqlCompiler = compiler;
            _dbConnection = connection;
            _database = new QueryFactory(_dbConnection, _sqlCompiler);
        }

        public async Task<int> Delete(DeleteDataRequestBody deleteDataRequestBody)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(InsertDataRequestBody insertDataRequestBody)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<dynamic>> Select(SelectDataRequestBody selectDataRequestBody)
        {
            var records = await _database.Query()
                .Select(selectDataRequestBody.ColumnNames.ToArray())
                .From(selectDataRequestBody.EntityName)
                .Join(joins: selectDataRequestBody.Joins)
                .Where(filtersGroups: selectDataRequestBody.FiltersGroups)
                .OrderBy(order: selectDataRequestBody.Order)
                .GetAsync();

            return records;
        }

        public async Task<int> Update(UpdateDataRequestBody updateDataRequestBody)
        {
            throw new NotImplementedException();
        }
    }
}
