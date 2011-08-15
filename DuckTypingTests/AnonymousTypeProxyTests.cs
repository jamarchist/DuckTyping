using System;
using System.Collections.Generic;
using System.Linq;
using Ducks;
using NUnit.Framework;
using DuckTypingProxy;

namespace DuckTypingTests
{
    [TestFixture]
    public class AnonymousTypeProxyTests
    {
        [Test]
        public void CanGetColorProperty()
        {
            var rubberDuck = new { Color = "yellow" };
            var typedRubberDuck = rubberDuck.As<IDuck>();

            Assert.That(typedRubberDuck.Color == "yellow");
        }

        [Test]
        public void CanSetColorProperty()
        {
            var lameDuck = new { Color = "whocares" };
            var typedLameDuck = lameDuck.As<IDuck>();

            typedLameDuck.Color = "blue";

            Assert.That("blue" == typedLameDuck.Color);
        }

        [Test]
        public void CanInvokeMethod()
        {
            var duck = new
            {
                Color = "white",
                Quack = (Func<string>)(() => "Quack!")
            };

            var typedDuck = duck.As<IDuck>();

            Assert.That("Quack!" == typedDuck.Quack());
        }

        [Test]
        public void CanInvokeOverloadedMethods()
        {
            var launchPad = new
            {
                Fly = new Delegate[]
                {
                    (Func<Direction>)(() => Direction.West),
                    (Func<Direction, Direction>)(d => d)
                }
            };

            var launchPadMcQuack = launchPad.As<IDuck>();

            Assert.That(Direction.West == launchPadMcQuack.Fly());
            Assert.That(Direction.South == launchPadMcQuack.Fly(Direction.South));
        }

        [Test]
        public void CanInvokeOverloadedMethodsWithSomewhatAmbiguousSignatures()
        {
            var zombie = new
            {
                Quack = new Delegate[]
                {
                    (Func<int, string>)(i => String.Format("{0} BRAAAINS!", i)),
                    (Func<string, string>)(s => String.Format("{0} BRAAAINS!", s)),
                    (Func<object, string>)(o => String.Format("{0}", o)),
                    (Func<object[], string>)(o => String.Format("{0} BRAAAIN OBJECTS!", o.Count()))
                }
            }.As<IDuck>();

            object brains = "BRAAAINS!";

            Assert.That("10 BRAAAINS!" == zombie.Quack(10));
            Assert.That("EAT BRAAAINS!" == zombie.Quack("EAT"));
            Assert.That("BRAAAINS!" == zombie.Quack(brains));
            Assert.That("3 BRAAAIN OBJECTS!" == zombie.Quack(brains, brains, brains));
        }

        [Test]
        public void CanInvokeIndexGetter()
        {
            var buckshot = new object();

            var duck = new
            {
                Item = new Delegate[]
                {
                    (Func<string, object>)(s => buckshot)
                }                     
            }.As<IDuck>();

            Assert.AreSame(buckshot, duck["weakness"]);
        }

        [Test]
        public void CanInvokeIndexSetter()
        {
            var isInfected = false;
            var duck = new
            {
                Item = new Delegate[]
                {
                    (Action<string, object>)((key,value) => isInfected = true)               
                }   
            }.As<IDuck>();

            duck["disease"] = "bird flu";
            Assert.IsTrue(isInfected);
        }

        [Test]
        public void CanStoreAndRetrieveWithIndexedProperty()
        {
            var store = new Dictionary<string, object>();
            var duck = new
            {
                Item = new Delegate[]
                {
                    (Func<string, object>)(s => store[s]),
                    (Action<string, object>)((s,o) => store[s] = o)
                }
            }.As<IDuck>();

            var feathers = new object();
            duck["feathers"] = feathers;
            
            Assert.AreSame(feathers, duck["feathers"]);
        }
    }
}
