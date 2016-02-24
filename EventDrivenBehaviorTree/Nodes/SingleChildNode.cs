using System;

namespace EventDrivenBehaviorTree.Nodes
{
    abstract class SingleChildNode : Node
    {
        Node child;

        protected SingleChildNode(BehaviorTree tree, Node parent)
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
