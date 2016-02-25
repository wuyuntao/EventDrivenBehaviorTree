using EventDrivenBehaviorTree.Events;
using System;

namespace EventDrivenBehaviorTree.Nodes
{
    public abstract class Node
    {
        public readonly BehaviorTree Tree;
        public readonly ParentNode Parent;
        public bool IsRunning;

        protected Node(BehaviorTree tree, ParentNode parent)
        {
            Tree = tree;
            Parent = parent;
        }

        internal protected void Start()
        {
            if (IsRunning)
                throw new InvalidOperationException();

            IsRunning = true;
            OnStart();
        }

        internal protected void Abort()
        {
            if (!IsRunning)
                throw new InvalidOperationException();

            OnAbort();

            End(false);
        }

        protected void End(bool success)
        {
            if (!IsRunning)
                throw new InvalidOperationException();

            OnEnd(success);

            if (Parent != null)
                Parent.OnChildEnd(this, success);

        }

        protected abstract void OnStart();

        protected virtual void OnEnd(bool success)
        {
            IsRunning = false;
        }

        protected virtual void OnAbort()
        {
        }
    }
}