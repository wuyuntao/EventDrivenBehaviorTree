namespace EventDrivenBehaviorTree
{
    class SendEventNode : LeafNode
    {
        NodeEventArgs eventArgs;

        public SendEventNode(BehaviorTree tree, Node parent, NodeEventArgs eventArgs)
            : base(tree, parent)
        {
            this.eventArgs = eventArgs;
        }

        public override void Start()
        {
            Tree.SendEvent(this, eventArgs);
            Tree.SendEvent(this, new NodeFinishedEventArgs(true));
        }
    }
}
