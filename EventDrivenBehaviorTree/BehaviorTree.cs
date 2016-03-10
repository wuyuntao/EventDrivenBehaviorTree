using EventDrivenBehaviorTree.Events;
using EventDrivenBehaviorTree.Nodes;
using System;
using System.Collections.Generic;

namespace EventDrivenBehaviorTree
{
    public class BehaviorTree : EventBus.IPublisher
    {
        Node m_root;
        EventBus m_eventBus = new EventBus();
        List<Node> m_pendingQueue = new List<Node>();

        List<Timer> m_timers = new List<Timer>();
        uint m_time;

        public BehaviorTree()
        {
        }

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

            if (m_pendingQueue.Count > 0)
            {
                for (int i = 0; i < m_pendingQueue.Count; i++)
                {
                    var node = m_pendingQueue[i];
                    node.Update();
                }

                m_pendingQueue.Clear();
                m_pendingQueue.TrimExcess();
            }
        }

        internal void Enqueue(Node node)
        {
            m_pendingQueue.Add(node);
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

        public Node Root
        {
            get { return m_root; }
            internal set
            {
                if (m_root != null)
                    throw new InvalidOperationException();

                m_root = value;
            }
        }

        public EventBus EventBus
        {
            get { return m_eventBus; }
        }
    }
}
