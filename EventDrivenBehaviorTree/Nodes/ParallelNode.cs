using System;
using System.Linq;

namespace EventDrivenBehaviorTree.Nodes
{
    class ParallelNode : MultiChildNode
    {
        bool[] childrenCompleted;

        public ParallelNode(BehaviorTree tree, ParentNode parent)
            : base(tree, parent)
        {
        }

        protected override void OnStart()
        {
            childrenCompleted = new bool[Children.Length];

            foreach (var child in Children)
                child.Start();
        }

        protected override void OnAbort()
        {
            AbortChildren();

            base.OnAbort();
        }

        internal override void OnChildEnd(Node child, bool success)
        {
            var index = Array.FindIndex(Children, c => c == child);
            if (index >= 0)
            {
                if (success)
                {
                    childrenCompleted[index] = success;
                    if (childrenCompleted.All(c => c))
                        OnEnd(true);
                }
                else
                {
                    AbortChildren();
                    OnEnd(false);
                }
            }
        }

        private void AbortChildren()
        {
            for (int i = 0; i < Children.Length; i++)
            {
                if (!childrenCompleted[i])
                    Children[i].Abort();
            }
        }
    }
}
