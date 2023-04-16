using Inflow.Data.DTO.DataRequestBodyItems;

namespace Inflow.Data.DTO
{
    public class DeleteDataRequestBody : BaseDataRequestBody
    {
        public IEnumerable<FiltersGroups>? FiltersGroups { get; set; }
    }
}
