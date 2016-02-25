using System;

namespace EventDrivenBehaviorTree.Nodes
{
    public abstract class MultiChildNode : ParentNode
    {
        Node[] children;

        protected MultiChildNode(BehaviorTree tree, ParentNode parent)
            : base(tree, parent)
        {
        }

        public Node[] Children
        {
            get { return children; }
            set
            {
                if (children != null)
                    throw new InvalidOperationException();

                children = value;
            }
        }
    }
}
