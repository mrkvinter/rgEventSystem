using System;

namespace rgEventSystem
{
    public interface IEventSystem
    {
        void Subscribe<TEvent>(Action<TEvent> handler)
            where TEvent : IEvent;

        void UnsubscribeAll<TEvent>()
            where TEvent : IEvent;

        void Unsubscribe<TEvent>(Action<TEvent> action)
            where TEvent : IEvent;

        void Notify<TEvent>(TEvent eventObj)
            where TEvent : IEvent;
    }
}