namespace EventDrivenBehaviorTree
{
    class ReturnFalseNode : SingleChildNode
    {
        public ReturnFalseNode(BehaviorTree tree, Node parent)
            : base(tree, parent)
        {
        }

        public override void OnStart()
        {
            Tree.OnNodeEvent += Tree_OnNodeEvent;
            Child.OnStart();
        }

        private void Tree_OnNodeEvent(Node sender, EventArgs eventArgs)
        {
            if (sender == Child && eventArgs is NodeFinishedEventArgs)
                OnEnd(false);
        }

        protected override void OnEnd(bool success)
        {
            Tree.OnNodeEvent -= Tree_OnNodeEvent;
            base.OnEnd(success);
        }
    }
}
