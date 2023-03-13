using Inflow.Data.DTO.DataRequestBodyItems;

namespace Inflow.Data.DTO
{
    public class UpdateDataRequestBody : BaseDataRequestBody
    {
        public IEnumerable<string> ColumnNames { get; set; }

        public IEnumerable<Filter>? Filters { get; set; }
    }
}
