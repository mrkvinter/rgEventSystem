using System;

namespace rgEventSystem
{
    public class EventHandler<TEvent> : IEventHandler
        where TEvent : IEvent
    {
        private event Action<TEvent> OnEvent;

        public void Add(Action<TEvent> action)
        {
            OnEvent += action;
        }

        public void Remove(Action<TEvent> action)
        {
            OnEvent -= action;
        }

        public void CallEvent(TEvent args)
        {
            OnEvent?.Invoke(args);
        }
    }
}