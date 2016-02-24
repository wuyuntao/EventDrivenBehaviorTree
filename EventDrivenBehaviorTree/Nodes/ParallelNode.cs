using System;
using System.Linq;

namespace EventDrivenBehaviorTree.Nodes
{
    class ParallelNode : MultiChildNode
    {
        bool[] childrenCompleted;

        public ParallelNode(BehaviorTree tree, Node parent)
            : base(tree, parent)
        {
        }

        public override void OnStart()
        {
            childrenCompleted = new bool[Children.Length];

            Tree.OnNodeEvent += Tree_OnNodeEvent;

            foreach (var child in Children)
            {
                child.OnStart();
            }
        }

        public override void OnAbort()
        {
            AbortChildren();
            base.OnAbort();
        }

        private void Tree_OnNodeEvent(Node sender, EventArgs eventArgs)
        {
            var index = Array.FindIndex(Children, c => c == sender);
            if (index >= 0 && eventArgs is NodeFinishedEventArgs)
            {
                var success = ((NodeFinishedEventArgs)eventArgs).Success;
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
                    Children[i].OnAbort();
            }
        }
    }
}
