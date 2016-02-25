namespace EventDrivenBehaviorTree.Nodes
{
    public abstract class ParentNode : Node
    {
        protected ParentNode(BehaviorTree tree, ParentNode parent)
            : base(tree, parent)
        {
        }

        internal abstract void OnChildEnd(Node child, bool success);
    }
}
