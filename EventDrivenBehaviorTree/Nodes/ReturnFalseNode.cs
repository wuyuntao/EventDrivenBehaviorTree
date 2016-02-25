namespace EventDrivenBehaviorTree.Nodes
{
    class ReturnFalseNode : SingleChildNode
    {
        public ReturnFalseNode(BehaviorTree tree, ParentNode parent)
            : base(tree, parent)
        {
        }

        protected override void OnStart()
        {
            Child.Start();
        }

        internal override void OnChildEnd(Node child, bool success)
        {
            OnEnd(false);
        }
    }
}
