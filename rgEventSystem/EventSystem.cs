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

        //todo-arseniy: кажется вместо List<object> можно хранить Action<TEvent>.
        private readonly Dictionary<Type, List<object>> colleagues =
            new Dictionary<Type, List<object>>();

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if (!colleagues.TryGetValue(eventType, out var list))
            {
                list = new List<object>();
                colleagues.Add(eventType, list);
            }

            list.Add(handler);
        }

        public void UnsubscribeAll<TEvent>()
            where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if (colleagues.ContainsKey(eventType))
                colleagues.Remove(eventType);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> eventObj)
            where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if (colleagues.ContainsKey(eventType))
                colleagues[eventType].Remove(eventObj);
        }

        public void Notify<TEvent>(TEvent eventObj) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            logger.Info($"{eventType.Name} happens.");
            if (colleagues.TryGetValue(eventType, out var actions))
            {
                foreach (var action in actions)
                {
                    var handler = action as Action<TEvent>;
                    if (handler == null)
                    {
                        logger.Error($"One of {eventType.Name} handlers is null.");
                        continue;
                    }

                    try
                    {
                        handler.Invoke(eventObj);
                    }
                    catch (Exception e)
                    {
                        logger.Error(e);
                    }
                }
            }
            else
            {
                logger.Warning($"Nothing subscribed on {eventType.Name}.");
            }
        }
    }
}