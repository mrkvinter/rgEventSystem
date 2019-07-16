using NUnit.Framework;
using rgEventSystem;
using rgEventSystem_Test.Events;
using rgEventSystem_Test.Tools;

namespace rgEventSystem_Test
{
    public class EventSystemTests
    {
        private EventSystem eventSystem;
        
        [SetUp]
        public void Setup()
        {
            var testLogger = new TestLogger();
            eventSystem = new EventSystem(testLogger);
        }

        [Test]
        public void SimpleWorkTest()
        {
            var number = 0;
            eventSystem.Subscribe<TestEvent>(e => { number += e.Number; });
            
            eventSystem.Notify(new TestEvent { Number = 2});
            eventSystem.Notify(new TestEvent { Number = 3});
            
            Assert.That(number, Is.EqualTo(5));
        }
    }
}