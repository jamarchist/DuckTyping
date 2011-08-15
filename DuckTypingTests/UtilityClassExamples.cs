using System;
using Ducks;
using DuckTypingProxy;
using NUnit.Framework;

namespace DuckTypingTests
{
    [TestFixture]
    public class UtilityClassExamples
    {
        [Test]
        public void CanPassAnonymousMethodsThroughUtilityClass()
        {
            var duck = new
            {
                Quack = Anon.Methods
                (
                    Anon.Method(() => "Quack!"),
                    Anon.Method<int, string>(i => String.Format("{0} quacks!", i))
                )
            };

            var typedDuck = duck.As<IDuck>();

            Assert.That("Quack!" == typedDuck.Quack());
        }
    }
}
