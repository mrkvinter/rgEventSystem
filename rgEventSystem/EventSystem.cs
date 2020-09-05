using System;
using System.Collections.Generic;
using rgEventSystem.Log;

namespace rgEventSystem
{
    public class EventSystem : IEventSystem
    {
        private readonly ILogger logger;

        public EventSystem(ILogger logger)
        {
            this.logger = logger;
        }

        private readonly Dictionary<Type, IEventHandler> colleagues =
            new Dictionary<Type, IEventHandler>();

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if (!colleagues.TryGetValue(eventType, out var eventHandler))
            {
                eventHandler = new EventHandler<TEvent>();
                colleagues.Add(eventType, eventHandler);
            }

            ((EventHandler<TEvent>)eventHandler).Add(handler);
        }

        public void UnsubscribeAll<TEvent>()
            where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if (colleagues.ContainsKey(eventType))
                colleagues.Remove(eventType);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> action)
            where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if (colleagues.ContainsKey(eventType))
                ((EventHandler<TEvent>)colleagues[eventType]).Remove(action);
        }

        public void Notify<TEvent>(TEvent eventObj) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            logger.Info($"{eventType.Name} happens.");
            if (colleagues.TryGetValue(eventType, out var eventHandler))
            {
                var concreteEventHandler = (EventHandler<TEvent>)eventHandler;
                concreteEventHandler.CallEvent(eventObj);
            }
            else
            {
                logger.Warning($"Nothing subscribed on {eventType.Name}.");
            }
        }
    }
}