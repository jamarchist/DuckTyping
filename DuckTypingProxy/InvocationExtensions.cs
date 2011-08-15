using Castle.Core.Interceptor;
using System.Linq;

namespace DuckTypingProxy
{
    internal static class InvocationExtensions
    {
        internal static bool IsGetter(this IInvocation methodCall)
        {
            return methodCall.Method.Name.StartsWithGet();
        }

        internal static bool IsSetter(this IInvocation methodCall)
        {
            return methodCall.Method.Name.StartsWithSet();
        }

        internal static bool IsIndexProperty(this IInvocation methodCall)
        {
            return (methodCall.IsGetter() || methodCall.IsSetter()) && methodCall.HasIndexParameters();
        }

        internal static bool IsMethodCall(this IInvocation methodCall)
        {
            return !methodCall.Method.Name.StartsWithGet() && !methodCall.Method.Name.StartsWithSet();
        }

        internal static string PropertyName(this IInvocation methodCall)
        {
            return methodCall.Method.Name.PropertyNamePart();
        }

        private static bool HasIndexParameters(this IInvocation methodCall)
        {
            return methodCall.Method.DeclaringType.GetProperty(methodCall.PropertyName()).GetIndexParameters().Count() > 0;
        }
    }
}