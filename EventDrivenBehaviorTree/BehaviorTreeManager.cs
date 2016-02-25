using EventDrivenBehaviorTree.Events;
using EventDrivenBehaviorTree.Nodes;
using System.Collections.Generic;

namespace EventDrivenBehaviorTree
{
    public class BehaviorTreeManager : EventBus.IPublisher
    {
        EventBus m_eventBus = new EventBus();
        List<Timer> m_timers = new List<Timer>();
        uint m_time;

        public void Update(uint deltaTime)
        {
            m_time += deltaTime;

            m_timers.RemoveAll(t =>
            {
                if (t.Time <= m_time)
                {
                    m_eventBus.Publish(this, new TimeoutEventArgs(t.GetHashCode()));
                    return true;
                }
                else
                    return false;
            });
        }

        public int SetTimer(Node node, uint time)
        {
            var timer = new Timer(node, time);
            m_timers.Add(timer);
            return timer.GetHashCode();
        }

        public void CancelTimer(int timerId)
        {
            m_timers.RemoveAll(t => t.GetHashCode() == timerId);
        }

        class Timer
        {
            public readonly uint Time;
            public readonly Node Node;

            public Timer(Node node, uint time)
            {
                Time = time;
                Node = node;
            }
        }
    }
}
