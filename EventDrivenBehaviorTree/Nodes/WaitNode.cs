using System.Collections.Generic;

namespace EventDrivenBehaviorTree.Nodes
{
    class WaitNode : LeafNode
    {
        readonly uint time;

        public WaitNode(BehaviorTree tree, Node parent, uint time)
            : base(tree, parent)
        {
            this.time = time;
        }

        protected override void OnStart()
        {
            Tree.Manager.SetTimer(this, time);
        }

        protected override bool? OnUpdate(out IEnumerable<Node> children)
        {
            children = null;

            return true;
        }
    }
}