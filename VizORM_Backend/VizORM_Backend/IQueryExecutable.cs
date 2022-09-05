using VizORM_Backend.DTO;

namespace VizORM_Backend
{
    public interface IQueryExecutable
    {
        Task<IEnumerable<dynamic>> SelectAsync(DataRequestBody dataRequestBody);

        Task<int> UpdateAsync(DataRequestBody dataRequestBody);

        Task<int> DeleteAsync(DataRequestBody dataRequestBody);

        Task<int> InsertAsync(DataRequestBody dataRequestBody);
    }
}
