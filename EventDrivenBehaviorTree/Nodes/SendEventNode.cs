namespace EventDrivenBehaviorTree
{
    class SendEventNode : LeafNode
    {
        EventArgs eventArgs;

        public SendEventNode(BehaviorTree tree, Node parent, EventArgs eventArgs)
            : base(tree, parent)
        {
            this.eventArgs = eventArgs;
        }

        public override void OnStart()
        {
            Tree.SendEvent(this, eventArgs);
            Tree.SendEvent(this, new NodeFinishedEventArgs(true));
        }
    }
}
