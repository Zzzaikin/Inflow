using SystemNotImplementedException = System.NotImplementedException;

namespace Inflow.Common.Exceptions
{
    public class NotImplementedException : SystemNotImplementedException
    {
        public string LocalizableStringName { get; }

        public string NotImplementedValue { get; }

        public NotImplementedException(string notImplementedValue, string localizableStringName, string message) 
            : base(message) 
        {
            LocalizableStringName = localizableStringName;
            NotImplementedValue = notImplementedValue;
        }
    }
}
