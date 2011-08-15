using Ducks;
using DuckTypingProxy;
using NUnit.Framework;

namespace DuckTypingTests
{
    [TestFixture]
    public class DuckTypeProxyTests
    {
        [Test]
        public void CanGetProperty()
        {
            var scrooge = new ScroogeMcDuck();
            scrooge.Color = "gold";

            var scroogeMcTyped = scrooge.As<IDuck>();

            Assert.That("gold" == scroogeMcTyped.Color);
        }

        [Test]
        public void CanSetProperty()
        {
            var scrooge = new ScroogeMcDuck();
            var scroogeMcTyped = scrooge.As<IDuck>();

            scroogeMcTyped.Color = "gold";

            Assert.That("gold" == scrooge.Color);
        }

        [Test]
        public void CanInvokeMethod()
        {
            var scrooge = new ScroogeMcDuck().As<IDuck>();

            Assert.That("Bah! Humbug!" == scrooge.Quack());
        }

        [Test]
        public void CanInvokeOverloadedMethods()
        {
            var scrooge = new ScroogeMcDuck().As<IDuck>();

            Assert.That(Direction.North == scrooge.Fly());
            Assert.That(Direction.East == scrooge.Fly(Direction.East));
        }

        [Test]
        public void CanInvokeOverloadedMethodsWithSomewhatAmbiguousSignatures()
        {
            var scrooge = new ScroogeMcDuck().As<IDuck>();

            object bah = "Bah!";

            Assert.That("Bah!Bah!Bah!" == scrooge.Quack(3));
            Assert.That("Humbug!" == scrooge.Quack("Humbug!"));
            Assert.That("Bah!" == scrooge.Quack(bah));
            Assert.That("Bah!Bah!" == scrooge.Quack(bah, bah));
        }

        [Test]
        public void CanInvokeIndexGetter()
        {
            var scrooge = new ScroogeMcDuck();
            scrooge["networth"] = "Billions";

            var scroogeMcTyped = scrooge.As<IDuck>();

            Assert.That("Billions" == scroogeMcTyped["networth"].ToString());
        }

        [Test]
        public void CanInvokeIndexSetter()
        {
            var scrooge = new ScroogeMcDuck();
            var scroogeMcTyped = scrooge.As<IDuck>();

            scroogeMcTyped["networth"] = "Billions";

            Assert.That("Billions" == scrooge["networth"].ToString());
        }

        [Test]
        public void CanStoreAndRetrieveWithIndexedProperty()
        {
            var scroogeMcTyped = new ScroogeMcDuck().As<IDuck>();

            scroogeMcTyped["networth"] = "Billions";

            Assert.That("Billions" == scroogeMcTyped["networth"].ToString());
        }
    }
}
