namespace EventDrivenBehaviorTree
{
    class RepeaterNode : SingleChildNode
    {
        int totalCount;
        int count;

        public RepeaterNode(BehaviorTree tree, Node parent, int totalCount)
            : base(tree, parent)
        {
            this.totalCount = totalCount;
        }

        public override void OnStart()
        {
            count = 0;

            Tree.OnNodeEvent += Tree_OnNodeEvent;
            Child.OnStart();
        }

        private void Tree_OnNodeEvent(Node sender, EventArgs eventArgs)
        {
            if (sender == Child && eventArgs is NodeFinishedEventArgs)
            {
                if (++count >= totalCount)
                    OnEnd(true);
                else
                    Child.OnStart();
            }
        }

        protected override void OnEnd(bool success)
        {
            Tree.OnNodeEvent -= Tree_OnNodeEvent;

            base.OnEnd(success);
        }
    }
}
