namespace EventDrivenBehaviorTree
{
    class SequenceNode : MultiChildNode
    {
        int currentIndex;

        public SequenceNode(BehaviorTree tree, Node parent)
            : base(tree, parent)
        {
        }

        public override void Start()
        {
            currentIndex = 0;

            Tree.OnNodeEvent += Tree_OnNodeEvent;

            Children[currentIndex].Start();

        }

        private void Tree_OnNodeEvent(Node sender, NodeEventArgs eventArgs)
        {
            var child = Children[currentIndex];
            if (child == sender && eventArgs is NodeFinishedEventArgs)
            {
                var finishedEventArgs = (NodeFinishedEventArgs)eventArgs;
                if (finishedEventArgs.Success)
                {
                    if (++currentIndex < Children.Length)
                    {
                        Children[currentIndex].Start();
                    }
                    else
                    {
                        End(true);
                    }
                }
                else
                {
                    End(false);
                }
            }
        }

        protected override void End(bool success)
        {
            Tree.OnNodeEvent -= Tree_OnNodeEvent;

            base.End(success);
        }
    }
}
