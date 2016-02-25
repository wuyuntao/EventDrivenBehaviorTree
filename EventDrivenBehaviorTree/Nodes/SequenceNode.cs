using System.Collections.Generic;

namespace EventDrivenBehaviorTree.Nodes
{
    public class SequenceNode : MultiChildNode
    {
        int m_currentIndex;

        public SequenceNode(BehaviorTree tree, Node parent)
            : base(tree, parent)
        {
        }

        protected override void OnStart()
        {
            m_currentIndex = -1;
        }

        protected override bool? OnUpdate(out IEnumerable<Node> children)
        {
            children = null;

            if (m_currentIndex < 0)
            {
                m_currentIndex = 0;
                children = new[] { Children[m_currentIndex] };
                return null;
            }
            else
            {
                var child = Children[m_currentIndex];
                if (child.LastStatus.Value)
                {
                    if (++m_currentIndex < Children.Length)
                    {
                        children = new[] { Children[m_currentIndex] };
                        return null;
                    }
                    else
                    {
                        children = null;
                        return true;
                    }
                }
                else
                {
                    children = null;
                    return false;
                }
            }
        }

        protected override void OnEnd()
        {
            if (LastStatus == null)
                Children[m_currentIndex].Abort();

            base.OnEnd();
        }
    }
}
