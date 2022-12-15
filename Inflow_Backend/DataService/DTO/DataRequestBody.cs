using Inflow.DataService.DTO.DataRequestBodyItems;
using Inflow.Common;

namespace Inflow.DataService.DTO
{
    public class DataRequestBody
    {
        private string? _entityName;

        public IEnumerable<string>? ColumnNames { get; set; }

        public IEnumerable<Filter>? Filters { get; set; }

        public IEnumerable<Join>? Joins { get; set; }

        public string EntityName
        {
            get
            {
                return _entityName;
            }

            set
            {
                Argument.NotNullOrEmpty(value, nameof(EntityName));
                _entityName = value;
            }
        }

        public int From { get; set; }

        public int Count { get; set; }

        public Order? Order { get; set; }
    }
}
