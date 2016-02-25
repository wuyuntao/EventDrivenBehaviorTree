using System;
using System.Collections.Generic;
using EventDrivenBehaviorTree.Events;

namespace EventDrivenBehaviorTree.Nodes
{
    class WaitEventNode : LeafNode, EventBus.ISubscriber
    {
        Type eventType;

        public WaitEventNode(BehaviorTree tree, Node parent, Type eventType)
            : base(tree, parent)
        {
            this.eventType = eventType;
        }

        protected override void OnStart()
        {
            Tree.EventBus.Subscribe(this, eventType);
        }

        protected override bool? OnUpdate(out IEnumerable<Node> children)
        {
            children = null;

            return true;
        }

        void EventBus.ISubscriber.OnEvent(EventBus.IPublisher publisher, EventArgs eventArgs)
        {
            Tree.EventBus.Unsubscribe(this);
        }
    }
}
