using System;
using System.Collections.Generic;

namespace EventDrivenBehaviorTree.Nodes
{
    public abstract class Node
    {
        public readonly BehaviorTree Tree;
        public readonly Node Parent;
        bool m_isRunning;
        bool? m_lastStatus;

        protected Node(BehaviorTree tree, Node parent)
        {
            Tree = tree;
            Parent = parent;
        }

        internal void Update()
        {
            if (!m_isRunning)
            {
                OnStart();
                m_isRunning = true;
                m_lastStatus = null;
            }

            IEnumerable<Node> children;
            var status = OnUpdate(out children);
            if (status != null)
            {
                m_lastStatus = status;
                m_isRunning = false;

                OnEnd();

                if (Parent != null)
                    Tree.Enqueue(Parent);
            }
            else if (children != null)
            {
                foreach (var child in children)
                    Tree.Enqueue(child);
            }
        }

        internal protected void Abort()
        {
            if (!m_isRunning)
                return;

            m_lastStatus = null;
            m_isRunning = false;

            OnEnd();
        }

        protected virtual void OnStart()
        { }

        protected abstract bool? OnUpdate(out IEnumerable<Node> children);

        protected virtual void OnEnd()
        { }

        public bool? LastStatus
        {
            get { return m_lastStatus; }
        }
    }
}