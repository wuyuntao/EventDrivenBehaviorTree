namespace EventDrivenBehaviorTree
{
    abstract class Node
    {
        public readonly BehaviorTree Tree;
        public readonly Node Parent;

        public abstract void Start();

        public virtual void Abort()
        {
            End(false);
        }

        protected Node(BehaviorTree tree, Node parent)
        {
            Tree = tree;
        }

        protected virtual void End(bool success)
        {
            Tree.SendEvent(this, new NodeFinishedEventArgs(success));
        }
    }
}
