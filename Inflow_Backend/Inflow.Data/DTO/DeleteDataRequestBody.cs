using Inflow.Data.DTO.DataRequestBodyItems;

namespace Inflow.Data.DTO
{
    public class DeleteDataRequestBody
    {
        public IEnumerable<Filter>? Filters { get; set; }
    }
}
