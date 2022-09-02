using VizORM_Common;

namespace VizORM_Backend.Bodies
{
    public class DataRequestBody
    {
        private string _entityName;

        public List<string>? Columns { get; set; }

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
    }
}
