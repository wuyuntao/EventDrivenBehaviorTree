using System;

namespace EventDrivenBehaviorTree.Nodes
{
    abstract class MultiChildNode : Node
    {
        Node[] children;

        protected MultiChildNode(BehaviorTree tree, Node parent)
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
