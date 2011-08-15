using System;
using System.Collections.Generic;
using Castle.Core.Interceptor;
using System.Linq;

namespace DuckTypingProxy
{
    internal class DuckTypingInterceptor : IInterceptor
    {
        private readonly object anonymous;
        private readonly IDictionary<string, object> properties = new Dictionary<string, object>();

        public DuckTypingInterceptor(object anonymous)
        {
            this.anonymous = anonymous;
        }

        public void Intercept(IInvocation invocation)
        {
            var forwarder = GetForwardingStrategy(invocation, anonymous, properties);
            forwarder.Execute();
        }

        private static IForwardingStrategy GetForwardingStrategy(
            IInvocation invocation, object anonymous, IDictionary<string, object> properties)
        {
            var forwardingStrategies = GetForwardingStrategyFactory(invocation, anonymous, properties);

            if (invocation.IsIndexProperty())
            {
                return forwardingStrategies.IndexedPropertyAccessor;
            }

            if (invocation.IsGetter())
            {
                return forwardingStrategies.PropertyGetter;
            }

            if (invocation.IsSetter())
            {
                return forwardingStrategies.PropertySetter;
            }

            if (invocation.IsMethodCall())
            {
                return forwardingStrategies.MethodInvoker;
            }

            return new DoNothing();
        }

        private static IForwardingStrategyFactory GetForwardingStrategyFactory(IInvocation invocation, object target, IDictionary<string, object> properties)
        {
            if (target.IsCompilerGeneratedType())
            {
                return new AnonymousTypeForwardingStrategyFactory(invocation, target, properties);
            }
            else
            {
                return new DuckForwardingStrategyFactory(invocation, target);
            }
        }

        private interface IForwardingStrategy
        {
            void Execute();
        }

        private abstract class BaseAnonymousTypeForwardingStrategy : IForwardingStrategy
        {
            private readonly object anonymous;
            private readonly IInvocation invocation;
            private readonly IDictionary<string, object> properties;

            protected BaseAnonymousTypeForwardingStrategy(IInvocation invocation, object anonymous, IDictionary<string, object> properties)
            {
                this.anonymous = anonymous;
                this.properties = properties;
                this.invocation = invocation;
            }

            protected IDictionary<string, object> Properties
            {
                get { return properties; }
            }

            protected IInvocation Invocation
            {
                get { return invocation; }
            }

            protected object Anonymous
            {
                get { return anonymous; }
            }

            public abstract void Execute();

            protected object GetPropertyValue(string property)
            {
                if (Properties.ContainsKey(property))
                {
                    return Properties[property];
                }

                return Anonymous.GetPropertyValue(property);
            }

            protected Delegate GetMethod(string methodName)
            {
                var methodProperty = Anonymous.GetPropertyValue(methodName);
                if (methodProperty is Delegate)
                {
                    return methodProperty as Delegate;
                }

                var overloads = methodProperty as Delegate[];
                return overloads.Where(m => m.Method.SignatureMatches(Invocation.Method)).First();
            }
        }

        private class AnonymousTypePropertyGetter : BaseAnonymousTypeForwardingStrategy
        {
            public AnonymousTypePropertyGetter(IInvocation invocation, object anonymous, IDictionary<string, object> properties) : 
                base(invocation, anonymous, properties)
            {
            }

            public override void Execute()
            {
                var property = Invocation.PropertyName();
                Invocation.ReturnValue = GetPropertyValue(property);                
            }
        }

        private class AnonymousTypePropertySetter : BaseAnonymousTypeForwardingStrategy
        {
            public AnonymousTypePropertySetter(IInvocation invocation, object anonymous, IDictionary<string, object> properties) : 
                base(invocation, anonymous, properties)
            {
            }

            public override void Execute()
            {
                Properties[Invocation.PropertyName()] = Invocation.Arguments[0];
            }
        }

        private class AnonymousTypeMethodInvoker : BaseAnonymousTypeForwardingStrategy
        {
            public AnonymousTypeMethodInvoker(IInvocation invocation, object anonymous, IDictionary<string, object> properties) : 
                base(invocation, anonymous, properties)
            {
            }

            public override void Execute()
            {
                var method = GetMethod(Invocation.Method.Name);
                Invocation.ReturnValue = method.DynamicInvoke(Invocation.Arguments);
            }
        }

        private class AnonymousTypeIndexedPropertyAccessor : BaseAnonymousTypeForwardingStrategy
        {
            public AnonymousTypeIndexedPropertyAccessor(IInvocation invocation, object anonymous, IDictionary<string, object> properties) : 
                base(invocation, anonymous, properties)
            {
            }

            public override void Execute()
            {
                var accessor = GetMethod(Invocation.PropertyName());
                Invocation.ReturnValue = accessor.DynamicInvoke(Invocation.Arguments);
            }
        }

        private class DoNothing : IForwardingStrategy
        {
            public void Execute()
            {
                // No suitable forwarding stategy found
            }
        }

        private interface IForwardingStrategyFactory
        {
            IForwardingStrategy PropertyGetter { get; }
            IForwardingStrategy PropertySetter { get; }
            IForwardingStrategy MethodInvoker { get; }
            IForwardingStrategy IndexedPropertyAccessor { get; }
        }

        private class AnonymousTypeForwardingStrategyFactory : IForwardingStrategyFactory
        {
            private readonly IInvocation invocation;
            private readonly object anonymous;
            private readonly IDictionary<string, object> properties;

            public AnonymousTypeForwardingStrategyFactory(
                IInvocation invocation, object anonymous, IDictionary<string, object> properties)
            {
                this.invocation = invocation;
                this.properties = properties;
                this.anonymous = anonymous;
            }

            public IForwardingStrategy PropertyGetter
            {
                get { return new AnonymousTypePropertyGetter(invocation, anonymous, properties); }
            }

            public IForwardingStrategy PropertySetter
            {
                get { return new AnonymousTypePropertySetter(invocation, anonymous, properties); }
            }

            public IForwardingStrategy MethodInvoker
            {
                get { return new AnonymousTypeMethodInvoker(invocation, anonymous, properties); }
            }

            public IForwardingStrategy IndexedPropertyAccessor
            {
                get { return new AnonymousTypeIndexedPropertyAccessor(invocation, anonymous, properties); }
            }
        }
    
        private class DuckForwardingStrategyFactory : IForwardingStrategyFactory
        {
            private readonly IInvocation invocation;
            private readonly object duck;

            public DuckForwardingStrategyFactory(IInvocation invocation, object duck)
            {
                this.invocation = invocation;
                this.duck = duck;
            }

            public IForwardingStrategy PropertyGetter
            {
                get { return new DuckPropertyGetter(invocation, duck); }
            }

            public IForwardingStrategy PropertySetter
            {
                get { return new DuckPropertySetter(invocation, duck); }
            }

            public IForwardingStrategy MethodInvoker
            {
                get { return new DuckMethodInvoker(invocation, duck); }
            }

            public IForwardingStrategy IndexedPropertyAccessor
            {
                get { return new DuckIndexedPropertyAccessor(invocation, duck); }
            }
        }

        private class DuckMethodInvoker : IForwardingStrategy
        {
            private readonly IInvocation invocation;
            private readonly object duck;

            public DuckMethodInvoker(IInvocation invocation, object duck)
            {
                this.invocation = invocation;
                this.duck = duck;
            }

            public void Execute()
            {
                var parameterTypes = invocation.Method.GetParameters().Select(p => p.ParameterType).ToArray();
                var method = duck.GetType().GetMethod(invocation.Method.Name, parameterTypes);
                invocation.ReturnValue = method.Invoke(duck, invocation.Arguments);
            }
        }

        private class DuckPropertyGetter : IForwardingStrategy
        {
            private readonly IInvocation invocation;
            private readonly object duck;

            public DuckPropertyGetter(IInvocation invocation, object duck)
            {
                this.invocation = invocation;
                this.duck = duck;
            }

            public void Execute()
            {
                invocation.ReturnValue = duck.GetPropertyValue(invocation.PropertyName());
            }
        }

        private class DuckPropertySetter : IForwardingStrategy
        {
            private readonly IInvocation invocation;
            private readonly object duck;

            public DuckPropertySetter(IInvocation invocation, object duck)
            {
                this.invocation = invocation;
                this.duck = duck;
            }

            public void Execute()
            {
                duck.GetType().GetProperty(invocation.PropertyName()).SetValue(duck, invocation.Arguments[0], null);
            }
        }

        private class DuckIndexedPropertyAccessor : IForwardingStrategy
        {
            private readonly IInvocation invocation;
            private readonly object duck;

            public DuckIndexedPropertyAccessor(IInvocation invocation, object duck)
            {
                this.invocation = invocation;
                this.duck = duck;
            }

            public void Execute()
            {
                if (invocation.IsGetter())
                {
                    new DuckIndexedPropertyGetter(invocation, duck).Execute();
                }
                else
                {
                    new DuckIndexedPropertySetter(invocation, duck).Execute();
                }
            }
        }

        private class DuckIndexedPropertyGetter : IForwardingStrategy
        {
            private readonly IInvocation invocation;
            private readonly object duck;

            public DuckIndexedPropertyGetter(IInvocation invocation, object duck)
            {
                this.invocation = invocation;
                this.duck = duck;
            }

            public void Execute()
            {
                invocation.ReturnValue = duck.GetType().GetProperty(invocation.PropertyName()).GetValue(duck, invocation.Arguments);
            }
        }

        private class DuckIndexedPropertySetter : IForwardingStrategy
        {
            private readonly IInvocation invocation;
            private readonly object duck;

            public DuckIndexedPropertySetter(IInvocation invocation, object duck)
            {
                this.invocation = invocation;
                this.duck = duck;
            }

            public void Execute()
            {
                var value = invocation.Arguments[invocation.Arguments.Length - 1];
                var indeces = invocation.Arguments.Take(invocation.Arguments.Length - 1).ToArray();
                duck.GetType().GetProperty(invocation.PropertyName()).SetValue(duck, value, indeces);
            }
        }
    }
}
