using System;
using System.Collections.Generic;

namespace EventDrivenBehaviorTree.Nodes
{
    class ReturnFalseNode : SingleChildNode
    {
        public ReturnFalseNode(BehaviorTree tree, Node parent)
            : base(tree, parent)
        {
        }

        protected override void OnStart()
        {
            base.OnStart();

            Tree.Enqueue(Child);
        }

        protected override bool? OnUpdate(out IEnumerable<Node> children)
        {
            children = null;

            return false;
        }
    }
}
