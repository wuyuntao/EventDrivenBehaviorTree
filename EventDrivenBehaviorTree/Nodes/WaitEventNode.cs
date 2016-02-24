using System;
using EventDrivenBehaviorTree.Events;

namespace EventDrivenBehaviorTree.Nodes
{
    class WaitEventNode : LeafNode
    {
        Type eventType;

        public WaitEventNode(BehaviorTree tree, Node parent, Type eventType)
            : base(tree, parent)
        {
            this.eventType = eventType;
        }

        protected override void OnStart()
        {
            Tree.Subscribe(this, eventType);
        }

        protected override void OnEvent(EventObject publisher, EventArgs eventArgs)
        {
            End(true);
        }

        protected override void OnEnd(bool success)
        {
            Tree.Unsubscribe(this);

            base.OnEnd(success);
        }
    }
}
