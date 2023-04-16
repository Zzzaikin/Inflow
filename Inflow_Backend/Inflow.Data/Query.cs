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
            Argument.IsNotNull(compiler, nameof(compiler));
            Argument.IsNotNull(connection, nameof(connection));

            _sqlCompiler = compiler;
            _dbConnection = connection;
            _database = new QueryFactory(_dbConnection, _sqlCompiler);
        }

        public async Task<int> DeleteAsync(DeleteDataRequestBody deleteDataRequestBody)
        {
            var affectedRecordCount = await _database.Query(deleteDataRequestBody.EntityName)
                .Where(filtersGroups: deleteDataRequestBody.FiltersGroups)
                .DeleteAsync();

            return affectedRecordCount;
        }

        
        public async Task<IEnumerable<string>> InsertAsync(InsertDataRequestBody insertDataRequestBody)
        {
            var insertedRecordsIds = await _database.Query(insertDataRequestBody.EntityName)
                .InsertManyGetIdsAsync(insertDataRequestBody.InsertingData);

            return insertedRecordsIds;
        }

        public async Task<IEnumerable<dynamic>> SelectAsync(SelectDataRequestBody selectDataRequestBody)
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

        public async Task<int> UpdateAsync(UpdateDataRequestBody updateDataRequestBody)
        {
            var affectedRecordsCount = await _database.Query(updateDataRequestBody.EntityName)
                .Where(filtersGroups: updateDataRequestBody.FiltersGroups)
                .UpdateAsync(updateDataRequestBody.UpdatingData);

            return affectedRecordsCount;
        }
    }
}
