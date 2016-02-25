using System;

namespace EventDrivenBehaviorTree.Nodes
{
    class RepeaterNode : SingleChildNode
    {
        int totalCount;
        int count;

        public RepeaterNode(BehaviorTree tree, ParentNode parent, int totalCount)
            : base(tree, parent)
        {
            this.totalCount = totalCount;
        }

        protected override void OnStart()
        {
            count = 0;

            Child.Start();
        }

        internal override void OnChildEnd(Node child, bool success)
        {
            if (++count >= totalCount)
                OnEnd(true);
            else
                Child.Start();
        }
    }
}