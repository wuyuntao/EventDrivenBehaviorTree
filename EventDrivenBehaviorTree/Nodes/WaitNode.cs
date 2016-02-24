using System;
using EventDrivenBehaviorTree.Events;

namespace EventDrivenBehaviorTree.Nodes
{
    class WaitNode : LeafNode
    {
        readonly uint time;
        int timerId;

        public WaitNode(BehaviorTree tree, Node parent, uint time)
            : base(tree, parent)
        {
            this.time = time;
        }

        protected override void OnStart()
        {
            timerId = Tree.SetTimer(this, time);

            Subscribe(this, typeof(TimeoutEventArgs));
        }

        protected override void OnEvent(EventObject publisher, EventArgs eventArgs)
        {
            Unsubscribe(this);
            End(true);
        }
    }
}