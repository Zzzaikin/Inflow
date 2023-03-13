namespace Inflow.Common
{
    public static class Argument
    {
        public static void NotNullOrEmpty(string argumentValue, string argumentName)
        {//TODO: Test thread culter info.
            if (string.IsNullOrEmpty(argumentValue))
            {
                var exceprionMessage = string.Format(GlobalResource.ArgumentCanNotBeNullOrEmpty, argumentName);
                throw new ArgumentException(exceprionMessage);
            }
        }

        public static void NotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                var exceprionMessage = string.Format(GlobalResource.ArgumentCanNotBeNull, argumentName);
                throw new ArgumentNullException(exceprionMessage);
            }
        }
    }
}
