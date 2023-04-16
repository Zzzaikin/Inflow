using Inflow.Data.DTO.DataRequestBodyItems;

namespace Inflow.Data.DTO
{
    public class UpdateDataRequestBody : BaseDataRequestBody
    {
        public Dictionary<string, string> UpdatingData { get; set; }

        public IEnumerable<FiltersGroups>? FiltersGroups { get; set; }
    }
}
