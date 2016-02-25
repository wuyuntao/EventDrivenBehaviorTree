namespace EventDrivenBehaviorTree.Nodes
{
    public class SequenceNode : MultiChildNode
    {
        int currentIndex;

        public SequenceNode(BehaviorTree tree, ParentNode parent)
            : base(tree, parent)
        {
        }

        protected override void OnStart()
        {
            currentIndex = 0;

            Children[currentIndex].Start();

        }

        internal override void OnChildEnd(Node child, bool success)
        {
            if (success)
            {
                if (++currentIndex < Children.Length)
                    Children[currentIndex].Start();
                else
                    OnEnd(true);
            }
            else
                OnEnd(false);
        }

        protected override void OnAbort()
        {
            Children[currentIndex].Abort();

            base.OnAbort();
        }
    }
}
