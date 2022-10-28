namespace VizORM.Common
{
    public class TooMuchSelectedRecordException : Exception 
    {
        public TooMuchSelectedRecordException(string? message) : base(message) { }

        public TooMuchSelectedRecordException(string? message, Exception? innerException) 
            : base(message, innerException) { }
    }
}
