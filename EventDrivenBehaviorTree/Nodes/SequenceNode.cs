namespace EventDrivenBehaviorTree
{
    class SequenceNode : MultiChildNode
    {
        int currentIndex;

        public SequenceNode(BehaviorTree tree, Node parent)
            : base(tree, parent)
        {
        }

        public override void OnStart()
        {
            currentIndex = 0;

            Tree.OnNodeEvent += Tree_OnNodeEvent;

            Children[currentIndex].OnStart();

        }

        private void Tree_OnNodeEvent(Node sender, EventArgs eventArgs)
        {
            var child = Children[currentIndex];
            if (child == sender && eventArgs is NodeFinishedEventArgs)
            {
                var finishedEventArgs = (NodeFinishedEventArgs)eventArgs;
                if (finishedEventArgs.Success)
                {
                    if (++currentIndex < Children.Length)
                    {
                        Children[currentIndex].OnStart();
                    }
                    else
                    {
                        OnEnd(true);
                    }
                }
                else
                {
                    OnEnd(false);
                }
            }
        }

        protected override void OnEnd(bool success)
        {
            Tree.OnNodeEvent -= Tree_OnNodeEvent;

            base.OnEnd(success);
        }
    }
}
