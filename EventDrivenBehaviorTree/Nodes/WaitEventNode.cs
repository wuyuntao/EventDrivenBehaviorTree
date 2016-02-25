using System;
using EventDrivenBehaviorTree.Events;

namespace EventDrivenBehaviorTree.Nodes
{
    class WaitEventNode : LeafNode, EventBus.ISubscriber
    {
        Type eventType;

        public WaitEventNode(BehaviorTree tree, ParentNode parent, Type eventType)
            : base(tree, parent)
        {
            this.eventType = eventType;
        }

        protected override void OnStart()
        {
            Tree.EventBus.Subscribe(this, eventType);
        }

        void EventBus.ISubscriber.OnEvent(EventBus.IPublisher publisher, EventArgs eventArgs)
        {
            if (eventArgs.GetType() == eventType)
            {
                Tree.EventBus.Unsubscribe(this);
                End(true);
            }
        }
    }
}
