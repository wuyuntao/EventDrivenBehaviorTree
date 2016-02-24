namespace EventDrivenBehaviorTree
{
    abstract class LeafNode : Node
    {
        protected LeafNode(BehaviorTree tree, Node parent)
            : base(tree, parent)
        {
        }
    }
}
