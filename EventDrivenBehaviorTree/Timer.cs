namespace EventDrivenBehaviorTree
{
    class Timer
    {
        public readonly uint Time;
        public readonly Node Node;

        public Timer(Node node, uint time)
        {
            Time = time;
            Node = node;
        }
    }
}
