using EventDrivenBehaviorTree.Events;
using EventDrivenBehaviorTree.Nodes;
using System;
using System.Collections.Generic;

namespace EventDrivenBehaviorTree
{
    public class BehaviorTree : EventBus.IPublisher
    {
        BehaviorTreeManager m_manager;
        Node m_root;
        EventBus m_eventBus = new EventBus();
        List<Node> m_pendingQueue = new List<Node>();

        public BehaviorTree(BehaviorTreeManager manager)
        {
            m_manager = manager;
        }

        public void Update()
        {
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

        public BehaviorTreeManager Manager
        {
            get { return m_manager; }
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
