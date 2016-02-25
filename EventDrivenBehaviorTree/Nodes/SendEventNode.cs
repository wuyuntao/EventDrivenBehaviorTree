using EventDrivenBehaviorTree.Events;
using System;
using System.Collections.Generic;

namespace EventDrivenBehaviorTree.Nodes
{
    class SendEventNode : LeafNode, EventBus.IPublisher
    {
        EventArgs eventArgs;

        public SendEventNode(BehaviorTree tree, Node parent, EventArgs eventArgs)
            : base(tree, parent)
        {
            this.eventArgs = eventArgs;
        }

        protected override void OnStart()
        {
            Tree.EventBus.Publish(this, eventArgs);
        }

        protected override bool? OnUpdate(out IEnumerable<Node> children)
        {
            children = null;
            return true;
        }
    }
}
