using VizORM_Common;

namespace VizORM_Backend.DTO.DataRequestBodyItems
{
    public class Join
    {
        private string _joiningEntityName;

        private string _leftColumnName;

        private string _rightColumnName;

        public string RightColumnName
        {
            get
            {
                return _rightColumnName;
            }

            set
            {
                Argument.NotNullOrEmpty(value, nameof(RightColumnName));
                _rightColumnName = value;
            }
        }

        public string LeftColumnName
        {
            get
            {
                return _leftColumnName;
            }

            set
            {
                Argument.NotNullOrEmpty(value, nameof(LeftColumnName));
                _leftColumnName = value;
            }
        }

        public string JoiningEntityName
        {
            get
            {
                return _joiningEntityName;
            }

            set
            {
                Argument.NotNullOrEmpty(value, nameof(JoiningEntityName));
                _joiningEntityName = value;
            }
        }

        public JoinType JoinType { get; set; }


    }
}
