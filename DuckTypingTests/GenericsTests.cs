using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DuckTypingProxy;

namespace DuckTypingTests
{
    [TestFixture]
    public class GenericsTests
    {
        public interface IDuck
        {
            string Paddle<T>(T something);
        }

        public interface IDuck<T>
        {
            string Paddle(T something);
        }

        [Test]
        public void CanInvokeGenericMethod()
        {
            var duck = new
            {
                Paddle = (Func<int, string>)(i => String.Format("Paddled {0} times.", i))
            }.As<IDuck>();
   
            Assert.That("Paddled 5 times." == duck.Paddle(5));
        }

        [Test]
        public void CanInvokeMethodOnGenericInterface()
        {
            var duck = new
            {
                Paddle = (Func<int, string>)(i => String.Format("Paddled {0} times.", i))
            }.As<IDuck<int>>();

            Assert.That("Paddled 4 times." == duck.Paddle(4));
        }
    }
}
