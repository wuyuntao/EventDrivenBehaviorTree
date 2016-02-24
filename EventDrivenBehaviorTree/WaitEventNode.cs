using System;

namespace EventDrivenBehaviorTree
{
    class WaitEventNode : LeafNode
    {
        Type eventType;

        public WaitEventNode(BehaviorTree tree, Node parent, Type eventType)
            : base(tree, parent)
        {
            this.eventType = eventType;
        }

        public override void Start()
        {
            Tree.OnNodeEvent += Tree_OnNodeEvent;
        }

        private void Tree_OnNodeEvent(Node sender, NodeEventArgs eventArgs)
        {
            if (eventArgs.GetType() == eventType)
            {
                End(true);
            }
        }

        protected override void End(bool success)
        {
            Tree.OnNodeEvent -= Tree_OnNodeEvent;

            base.End(success);
        }
    }
}
