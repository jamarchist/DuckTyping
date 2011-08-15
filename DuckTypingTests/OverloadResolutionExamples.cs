using System;
using NUnit.Framework;

namespace DuckTypingTests
{
    [TestFixture]
    public class OverloadResolutionExamples
    {
        public interface IParam { }
        class Param : IParam { }
        class OtherParam : IParam { }

        class Target
        {
            internal void M(IParam p)
            {
                Console.WriteLine("Inside M1");
            }

            internal void M(Param p)
            {
                Console.WriteLine("Inside M2");
            }

            internal void M(params object[] ps)
            {
                Console.WriteLine("Inside M3");
            }

            internal void M(object p)
            {
                Console.WriteLine("Inside M4");
            }

            internal void M(object p1, object p2)
            {
                Console.WriteLine("Inside M5");
            }

            internal void M(IParam p1, IParam p2)
            {
                Console.WriteLine("Inside M6");
            }

            internal void M(params IParam[] ps)
            {
                Console.WriteLine("Inside M7");
            }
        }

        [Test]
        public void A()
        {
            var target = new Target();
            var p = new Param();
            var p2 = new OtherParam();
            object x = new Param();

            target.M(p);
            target.M(p2);
            target.M(x);
            target.M(p, p2);
            target.M(x, x);
        }
    }
}
