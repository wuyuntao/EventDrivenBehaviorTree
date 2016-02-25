using EventDrivenBehaviorTree.Events;
using EventDrivenBehaviorTree.Nodes;
using System;
using System.Collections.Generic;

namespace EventDrivenBehaviorTree
{
    public class BehaviorTree : EventBus.IPublisher
    {
        Node root;
        EventBus m_eventBus = new EventBus();
        List<Timer> timers = new List<Timer>();
        uint time;

        public void Start()
        {
            root.Start();
        }

        public void Update(uint deltaTime)
        {
            time += deltaTime;

            timers.RemoveAll(t =>
            {
                if (t.Time <= time)
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
            timers.Add(timer);
            return timer.GetHashCode();
        }

        public void CancelTimer(int timerId)
        {
            timers.RemoveAll(t => t.GetHashCode() == timerId);
        }

        public Node Root
        {
            get { return root; }
            internal set
            {
                if (root != null)
                    throw new InvalidOperationException();

                root = value;
            }
        }

        public EventBus EventBus
        {
            get { return m_eventBus; }
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
