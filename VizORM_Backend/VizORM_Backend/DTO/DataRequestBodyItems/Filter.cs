using VizORM.Common;

namespace VizORM.DataService.DTO.DataRequestBodyItems
{
    public class Filter
    {
        private string _leftColumn;

        private string _rightColumn;

        public ComparisonType ComparisonType { get; set; }

        public string LeftColumn
        {
            get
            {
                return _leftColumn;
            }
            set
            {
                Argument.NotNullOrEmpty(value, nameof(LeftColumn));
                _leftColumn = value;
            }
        }

        public string RightColumn
        {
            get
            {
                return _rightColumn;
            }

            set
            {
                Argument.NotNullOrEmpty(value, nameof(RightColumn));
                _rightColumn = value;
            }
        }
    }
}
