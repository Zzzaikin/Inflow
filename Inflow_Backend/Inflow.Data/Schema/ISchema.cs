using Inflow.Data.Models;

namespace Inflow.Data.Schema
{
    public interface ISchema
    {
        Task GetAsync(string name);

        Task<IEnumerable<SectionsDisplayedInNav>> GetSectionsDisplayedInNavAsync(int limit, int offset);
    }
}
