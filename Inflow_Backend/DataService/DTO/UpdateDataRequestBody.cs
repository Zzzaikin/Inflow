using Inflow.DataService.DTO.DataRequestBodyItems;

namespace Inflow.DataService.DTO
{
    public class UpdateDataRequestBody : BaseDataRequestBody
    {
        public IEnumerable<string> ColumnNames { get; set; }

        public IEnumerable<Filter>? Filters { get; set; }
    }
}
