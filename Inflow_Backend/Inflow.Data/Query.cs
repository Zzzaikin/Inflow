﻿using SqlKata.Execution;
using Inflow.Data.Options;
using Inflow.Data.Extensions;
using Inflow.Data.DTO.DataRequest;

namespace Inflow.Data
{
    public class Query : BaseQuery, IDataQueryable
    {
        public Query(BaseSqlOptions sqlOptions) : base(sqlOptions) { }

        public async Task<int> DeleteAsync(DeleteDataRequestBody deleteDataRequestBody)
        {
            var affectedRecordCount = await Database.Query(deleteDataRequestBody.EntityName)
                .Where(filtersGroups: deleteDataRequestBody.FiltersGroups)
                .DeleteAsync();

            return affectedRecordCount;
        }

        public async Task<IEnumerable<string>> InsertAsync(InsertDataRequestBody insertDataRequestBody)
        {
            var insertedRecordsIds = await Database.Query(insertDataRequestBody.EntityName)
                .InsertManyGetIdsAsync(insertDataRequestBody.InsertingData);

            return insertedRecordsIds;
        }

        public async Task<IEnumerable<dynamic>> SelectAsync(SelectDataRequestBody selectDataRequestBody)
        {
            var records = await Database.Query()
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
            var affectedRecordsCount = await Database.Query(updateDataRequestBody.EntityName)
                .Where(filtersGroups: updateDataRequestBody.FiltersGroups)
                .UpdateAsync(updateDataRequestBody.UpdatingData);

            return affectedRecordsCount;
        }
    }
}
