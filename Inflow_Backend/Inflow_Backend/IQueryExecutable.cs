using Inflow.DataService.DTO;

namespace Inflow.DataService
{
    public interface IQueryExecutable
    {
        Task<IEnumerable<dynamic>> SelectAsync(DataRequestBody dataRequestBody);

        Task<int> UpdateAsync(DataRequestBody dataRequestBody);

        Task<int> DeleteAsync(DataRequestBody dataRequestBody);

        Task<int> InsertAsync(DataRequestBody dataRequestBody);
    }
}
