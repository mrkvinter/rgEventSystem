using NUnit.Framework;
using rgEventSystem;
using rgEventSystem_Test.Events;
using rgEventSystem_Test.Tools;
using System;

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
        public void CallEvent_TwoCalls_CorrectNumber()
        {
            var number = 0;
            eventSystem.Subscribe<TestEvent>(e => number += e.Number);
            
            eventSystem.Notify(new TestEvent { Number = 2});
            eventSystem.Notify(new TestEvent { Number = 3});
            
            Assert.That(number, Is.EqualTo(5));
        }

        [Test]
        public void CallEvent_EmptySubscribers_NoException()
        {
            Assert.DoesNotThrow(() => eventSystem.Notify(new TestEvent()));
        }

        [Test]
        public void CallEvent_Unsubscribe_CorrectCall()
        {
            var number = 0;
            void EventAction(TestEvent e) => number += e.Number;
            eventSystem.Subscribe<TestEvent>(EventAction);

            eventSystem.Notify(new TestEvent { Number = 2 });
            eventSystem.Unsubscribe<TestEvent>(EventAction);
            eventSystem.Notify(new TestEvent { Number = 3 });

            Assert.That(number, Is.EqualTo(2));
        }
    }
}