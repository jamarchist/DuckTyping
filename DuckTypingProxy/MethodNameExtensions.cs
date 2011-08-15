namespace DuckTypingProxy
{
    internal static class MethodNameExtensions
    {
        internal static bool StartsWithGet(this string methodName)
        {
            return methodName.StartsWith("get_");
        }

        internal static bool StartsWithSet(this string methodName)
        {
            return methodName.StartsWith("set_");
        }

        internal static string PropertyNamePart(this string methodName)
        {
            return methodName.Substring(4, methodName.Length - 4);
        }
    }
}
