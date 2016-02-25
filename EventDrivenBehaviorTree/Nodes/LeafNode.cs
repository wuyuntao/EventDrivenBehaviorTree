namespace EventDrivenBehaviorTree.Nodes
{
    abstract class LeafNode : Node
    {
        protected LeafNode(BehaviorTree tree, ParentNode parent)
            : base(tree, parent)
        { }
    }
}
