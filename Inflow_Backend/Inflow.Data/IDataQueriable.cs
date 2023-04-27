using Inflow.Data.DTO.DataRequest;

namespace Inflow.Data
{
    internal interface IDataQueriable
    {
        Task<int> DeleteAsync(DeleteDataRequestBody deleteDataRequestBody);

        Task<IEnumerable<string>> InsertAsync(InsertDataRequestBody insertDataRequestBody);

        Task<int> UpdateAsync(UpdateDataRequestBody updateDataRequestBody);

        Task<IEnumerable<dynamic>> SelectAsync(SelectDataRequestBody selectDataRequestBody);
    }
}
