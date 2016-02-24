using System;

namespace EventDrivenBehaviorTree.Events
{
    class TimeoutEventArgs : EventArgs
    {
        public readonly int TimerId;

        public TimeoutEventArgs(int timerId)
        {
            TimerId = timerId;
        }
    }
}
