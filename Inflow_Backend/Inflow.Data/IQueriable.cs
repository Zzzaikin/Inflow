﻿using Inflow.Data.DTO;

namespace Inflow.Data
{
    internal interface IQueriable
    {
        Task<int> DeleteAsync(DeleteDataRequestBody deleteDataRequestBody);

        Task<IEnumerable<string>> InsertAsync(InsertDataRequestBody insertDataRequestBody);

        Task<int> UpdateAsync(UpdateDataRequestBody updateDataRequestBody);

        Task<IEnumerable<dynamic>> SelectAsync(SelectDataRequestBody selectDataRequestBody);
    }
}
