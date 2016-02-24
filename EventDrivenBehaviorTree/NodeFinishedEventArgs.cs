namespace EventDrivenBehaviorTree
{
    class NodeFinishedEventArgs : NodeEventArgs
    {
        public readonly bool Success;

        public NodeFinishedEventArgs(bool success)
        {
            Success = success;
        }
    }
}
