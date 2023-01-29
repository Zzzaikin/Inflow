namespace Inflow.DataService.DTO.DataRequestBodyItems
{
    public class FiltersGroups
    {
        public ConditionalOperator ConditionalOperator { get; set; }

        public IEnumerable<Filter> Filters { get; set; }
    }
}
