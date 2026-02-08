using System;

namespace UnigineApp.data.Events
{

    internal enum ACHIEVEMENTS_EVENT
    {
        EVENT_CLEARED,
        HIDDEN_EVENT_CLEARED
    }

    internal enum PLAYER_STATE_EVENT
    {
        NORMAL,
        POISONED,
        DEAD
    }

    internal struct DAMAGE_EVENT
    {
        public int amount;
        public DAMAGE_EVENT(int amount) => this.amount = amount;
    }

    internal class Event<T>
    {
        public event Action<T>? OnEvent;

        public void RunEvent(T value)
        {
            OnEvent?.Invoke(value);
        }
    }

    internal sealed class EventManager
    {
        public static EventManager Instance => _instance.Value;

        public Event<ACHIEVEMENTS_EVENT> Achievement { get; } = new Event<ACHIEVEMENTS_EVENT>();
        public Event<PLAYER_STATE_EVENT> State { get; } = new Event<PLAYER_STATE_EVENT>();
        public Event<DAMAGE_EVENT> Damage { get; } = new Event<DAMAGE_EVENT>();

        private EventManager() { }
        private static readonly Lazy<EventManager> _instance = new Lazy<EventManager>(() => new EventManager());
    }
}
