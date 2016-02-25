using EventDrivenBehaviorTree.Events;
using System;

namespace EventDrivenBehaviorTree.Nodes
{
    class SendEventNode : LeafNode, EventBus.IPublisher
    {
        EventArgs eventArgs;

        public SendEventNode(BehaviorTree tree, ParentNode parent, EventArgs eventArgs)
            : base(tree, parent)
        {
            this.eventArgs = eventArgs;
        }

        protected override void OnStart()
        {
            Tree.EventBus.Publish(this, eventArgs);
        }
    }
}
