namespace VizORM_Common
{
    public static class Argument
    {
        public static void NotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentName))
                throw new ArgumentNullException(nameof(argumentName));
        }

        public static void NotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(nameof(argumentName));
        }
    }
}
