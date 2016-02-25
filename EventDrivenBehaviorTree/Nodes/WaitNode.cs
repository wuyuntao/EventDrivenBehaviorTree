using System;
using EventDrivenBehaviorTree.Events;

namespace EventDrivenBehaviorTree.Nodes
{
    class WaitNode : LeafNode, EventBus.ISubscriber
    {
        readonly uint time;
        int timerId;

        public WaitNode(BehaviorTree tree, ParentNode parent, uint time)
            : base(tree, parent)
        {
            this.time = time;
        }

        protected override void OnStart()
        {
            timerId = Tree.SetTimer(this, time);

            Tree.EventBus.Subscribe(this, typeof(TimeoutEventArgs));
        }

        void EventBus.ISubscriber.OnEvent(EventBus.IPublisher publisher, EventArgs eventArgs)
        {
            var timeoutEventArgs = (eventArgs as TimeoutEventArgs);
            if (timeoutEventArgs != null && timerId == timeoutEventArgs.TimerId)
            {
                Tree.EventBus.Unsubscribe(this);

                End(true);
            }
        }
    }
}