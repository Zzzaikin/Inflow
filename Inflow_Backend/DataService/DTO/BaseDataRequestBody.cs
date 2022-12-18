using Inflow.Common;

namespace Inflow.DataService.DTO
{
    public class BaseDataRequestBody
    {
        private string _entityName;

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
