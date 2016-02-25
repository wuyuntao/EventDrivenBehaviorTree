using System.Collections.Generic;

namespace EventDrivenBehaviorTree.Nodes
{
    class ParallelNode : MultiChildNode
    {
        int m_completedCount;

        public ParallelNode(BehaviorTree tree, Node parent)
            : base(tree, parent)
        {
        }

        protected override void OnStart()
        {
            m_completedCount = 0;

            foreach (var child in Children)
                Tree.Enqueue(child);
        }

        protected override bool? OnUpdate(out IEnumerable<Node> children)
        {
            children = null;
            if (++m_completedCount >= Children.Length)
                return true;
            else
                return null;
        }

        protected override void OnEnd()
        {
            if (LastStatus == null)
            {
                foreach (var child in Children)
                    child.Abort();
            }

            base.OnEnd();
        }
    }
}
