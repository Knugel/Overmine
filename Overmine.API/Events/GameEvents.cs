using System;
using System.Collections.Generic;
using Thor;

namespace Overmine.API.Events
{
    public static class GameEvents
    {
        private static readonly Dictionary<Type, List<object>> EventHandlers =
            new Dictionary<Type, List<object>>();
        
        public static void Register<T>(Action<T> callback)
        {
            EventHandlers.TryGetValue(typeof(T), out var handlers);
            if (handlers == null)
                handlers = new List<object>();
            handlers.Add(callback);
            EventHandlers[typeof(T)] = handlers;
        }

        public static void Unregister<T>(Action<T> callback)
        {
            if (!EventHandlers.TryGetValue(typeof(T), out var handlers)) return;
            handlers.Remove(callback);
        }

        public static void Fire<T>(T value)
        {
            if (!EventHandlers.TryGetValue(typeof(T), out var handlers)) return;

            foreach (var handler in handlers)
            {
                var callback = handler as Action<T>;
                callback?.Invoke(value);
            }
        }

        public static void Register(SimulationEvent.EventType type, Action<SimulationEvent> handler)
        {
            Game.Instance.Simulation.RegisterEvent(type, handler);
        }

        public static void Unregister(SimulationEvent.EventType type, Action<SimulationEvent> handler)
        {
            Game.Instance.Simulation.UnregisterEvent(type, handler);
        }

        public static void Fire(SimulationEvent simEvent)
        {
            Game.Instance.Simulation.FireEvent(simEvent);
        }
    }
}