using Inflow.DataService.DTO.DataRequestBodyItems;

namespace Inflow.DataService.DTO
{
    public class DeleteDataRequestBody
    {
        public IEnumerable<Filter>? Filters { get; set; }
    }
}
