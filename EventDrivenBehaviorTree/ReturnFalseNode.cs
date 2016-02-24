namespace EventDrivenBehaviorTree
{
    class ReturnFalseNode : SingleChildNode
    {
        public ReturnFalseNode(BehaviorTree tree, Node parent)
            : base(tree, parent)
        {
        }

        public override void Start()
        {
            Tree.OnNodeEvent += Tree_OnNodeEvent;
            Child.Start();
        }

        private void Tree_OnNodeEvent(Node sender, NodeEventArgs eventArgs)
        {
            if (sender == Child && eventArgs is NodeFinishedEventArgs)
                End(false);
        }

        protected override void End(bool success)
        {
            Tree.OnNodeEvent -= Tree_OnNodeEvent;
            base.End(success);
        }
    }
}
