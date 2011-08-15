using System;
using System.Reflection;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DuckTypingProxy
{
    internal static class ReflectionExtensions
    {
        internal static object GetPropertyValue(this object target, string property)
        {
            return target.GetType().GetProperty(property).GetValue(target, null);
        }

        internal static bool IsCompilerGeneratedType(this object target)
        {
            var attributes = target.GetType().GetCustomAttributes(false);
            foreach (var attribute in attributes)
            {
                if (attribute is CompilerGeneratedAttribute)
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool SignatureMatches(this MethodInfo method, MethodInfo other)
        {
            var theseParameters = method.GetParameters();
            var thoseParameters = other.GetParameters();

            if (theseParameters.Count() != thoseParameters.Count())
            {
                return false;
            }

            foreach (var parameter in theseParameters)
            {
                var parameterIndex = Array.IndexOf(theseParameters, parameter);
                if (!parameter.ParameterType.Equals(thoseParameters[parameterIndex].ParameterType))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
