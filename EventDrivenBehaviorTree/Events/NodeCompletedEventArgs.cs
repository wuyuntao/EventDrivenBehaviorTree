using System;

namespace EventDrivenBehaviorTree.Events
{
    class NodeCompletedEventArgs : EventArgs
    {
        public readonly bool Success;

        public NodeCompletedEventArgs(bool success)
        {
            Success = success;
        }
    }
}
