using EventDrivenBehaviorTree.Events;
using System;

namespace EventDrivenBehaviorTree.Nodes
{
    abstract class Node : EventObject
    {
        public readonly BehaviorTree Tree;
        public readonly Node Parent;

        protected Node(BehaviorTree tree, Node parent)
        {
            Tree = tree;
            Parent = parent;

            Subscribe(tree, typeof(EventArgs));
        }

        public void Start()
        {
            OnStart();
        }

        public void Abort()
        {
            OnAbort();
            End(false);
        }

        public void End(bool success)
        {
            OnEnd(success);
        }

        protected abstract void OnStart();

        protected virtual void OnAbort()
        {
        }

        protected virtual void OnEnd(bool success)
        {
            Publish(new NodeCompletedEventArgs(success));
        }
    }
}