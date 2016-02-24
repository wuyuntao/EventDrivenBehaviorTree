using System;
using System.Linq;

namespace EventDrivenBehaviorTree
{
    class ParallelNode : MultiChildNode
    {
        bool[] childrenCompleted;

        public ParallelNode(BehaviorTree tree, Node parent)
            : base(tree, parent)
        {
        }

        public override void Start()
        {
            childrenCompleted = new bool[Children.Length];

            Tree.OnNodeEvent += Tree_OnNodeEvent;

            foreach (var child in Children)
            {
                child.Start();
            }
        }

        public override void Abort()
        {
            AbortChildren();
            base.Abort();
        }

        private void Tree_OnNodeEvent(Node sender, NodeEventArgs eventArgs)
        {
            var index = Array.FindIndex(Children, c => c == sender);
            if (index >= 0 && eventArgs is NodeFinishedEventArgs)
            {
                var success = ((NodeFinishedEventArgs)eventArgs).Success;
                if (success)
                {
                    childrenCompleted[index] = success;
                    if (childrenCompleted.All(c => c))
                        End(true);
                }
                else
                {
                    AbortChildren();
                    End(false);
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
