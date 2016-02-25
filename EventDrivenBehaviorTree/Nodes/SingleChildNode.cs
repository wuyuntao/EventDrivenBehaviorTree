using System;

namespace EventDrivenBehaviorTree.Nodes
{
    public abstract class SingleChildNode : ParentNode
    {
        Node child;

        protected SingleChildNode(BehaviorTree tree, ParentNode parent)
            : base(tree, parent)
        {
        }

        public Node Child
        {
            get { return child; }
            set
            {
                if (child != null)
                    throw new InvalidOperationException();

                child = value;
            }
        }
    }
}
