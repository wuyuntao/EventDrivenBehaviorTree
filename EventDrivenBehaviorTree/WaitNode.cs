namespace EventDrivenBehaviorTree
{
    class WaitNode : LeafNode
    {
        uint time;
        int timerId;

        public WaitNode(BehaviorTree tree, Node parent, uint time)
            : base(tree, parent)
        {
            this.time = time;
        }

        public override void Start()
        {
            timerId = Tree.SetTimer(this, time);

            Tree.OnNodeEvent += Tree_OnNodeEvent;
        }

        void Tree_OnNodeEvent(Node sender, NodeEventArgs eventArgs)
        {
            if (sender == this && eventArgs is TimeoutEventArgs && ((TimeoutEventArgs)eventArgs).TimerId == timerId)
                End(true);
        }

        protected override void End(bool success)
        {
            Tree.OnNodeEvent -= Tree_OnNodeEvent;

            base.End(success);
        }
    }
}
