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

        public override void Start()
        {
            count = 0;

            Tree.OnNodeEvent += Tree_OnNodeEvent;
            Child.Start();
        }

        private void Tree_OnNodeEvent(Node sender, NodeEventArgs eventArgs)
        {
            if (sender == Child && eventArgs is NodeFinishedEventArgs)
            {
                if (++count >= totalCount)
                    End(true);
                else
                    Child.Start();
            }
        }

        protected override void End(bool success)
        {
            Tree.OnNodeEvent -= Tree_OnNodeEvent;

            base.End(success);
        }
    }
}
