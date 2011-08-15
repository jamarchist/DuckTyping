using Castle.DynamicProxy;

namespace DuckTypingProxy
{
    public static class DuckTypingExtensions
    {
        public static TDuck As<TDuck>(this object anonymous) where TDuck : class
        {
            var proxyGenerator = new ProxyGenerator();
            var objectImplementingDuckInterface =
                proxyGenerator.CreateInterfaceProxyWithoutTarget(typeof(TDuck), new DuckTypingInterceptor(anonymous));

            return objectImplementingDuckInterface as TDuck;
        }
    }
}
