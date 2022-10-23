using DataService.DTO.DataRequestBodyItems;
using VizORM_Backend.DTO.DataRequestBodyItems;
using VizORM_Common;

namespace VizORM_Backend.DTO
{
    public class DataRequestBody
    {
        private string? _entityName;

        public List<string>? ColumnNames { get; set; }

        public List<Filter>? Filters { get; set; }

        public List<Join>? Joins { get; set; }

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
