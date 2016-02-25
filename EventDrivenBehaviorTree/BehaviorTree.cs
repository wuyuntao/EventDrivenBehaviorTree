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
                var currentQueue = new List<Node>();
                while (m_pendingQueue.Count > 0)
                {
                    // Swap
                    var queue = currentQueue;
                    currentQueue = m_pendingQueue;
                    m_pendingQueue = queue;

                    // Update nodes
                    foreach (var node in currentQueue)
                        node.Update();

                    currentQueue.Clear();
                }
            }
        }

        internal void Enqueue(Node node)
        {
            if (!m_pendingQueue.Contains(node))
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
