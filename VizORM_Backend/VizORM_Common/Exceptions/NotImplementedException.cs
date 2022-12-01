using SystemNotImplementedException = System.NotImplementedException;

namespace VizORM.Common.Exceptions
{
    public class NotImplementedException : SystemNotImplementedException
    {
        public string LocalizableStringName { get; }

        public string NotImplementedValue { get; }

        public NotImplementedException(string notImplementedValue, string localizableStringName) 
            : base(localizableStringName) 
        {
            LocalizableStringName = localizableStringName;
            NotImplementedValue = notImplementedValue;
        }
    }
}
