using Inflow.Data.DTO;

namespace Inflow.Data
{
    internal interface IQueriable
    {
        Task<int> Delete(DeleteDataRequestBody deleteDataRequestBody);

        Task<int> Insert(InsertDataRequestBody insertDataRequestBody);

        Task<int> Update(UpdateDataRequestBody updateDataRequestBody);

        Task<IEnumerable<dynamic>> Select(SelectDataRequestBody selectDataRequestBody);
    }
}
