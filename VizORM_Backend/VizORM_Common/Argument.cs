namespace VizORM_Common
{
    public static class Argument
    {
        public static void NotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentName))
            {
                throw new ArgumentNullException(nameof(argumentName));
            }
        }
    }
}
