namespace EventDrivenBehaviorTree
{
    class TimeoutEventArgs : NodeEventArgs
    {
        public readonly int TimerId;

        public TimeoutEventArgs(int timerId)
        {
            TimerId = timerId;
        }
    }
}
