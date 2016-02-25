using System.Collections.Generic;

namespace EventDrivenBehaviorTree.Nodes
{
    class RepeaterNode : SingleChildNode
    {
        int m_totalCount;
        int m_count;

        public RepeaterNode(BehaviorTree tree, Node parent, int totalCount)
            : base(tree, parent)
        {
            m_totalCount = totalCount;
        }

        protected override void OnStart()
        {
            base.OnStart();

            m_count = 0;
        }

        protected override bool? OnUpdate(out IEnumerable<Node> children)
        {
            if (++m_count <= m_totalCount)
            {
                children = new[] { Child };
                return null;
            }
            else
            {
                children = null;
                return true;
            }
        }
    }
}