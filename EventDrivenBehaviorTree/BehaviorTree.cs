using System;
using System.Collections.Generic;

namespace EventDrivenBehaviorTree
{
    class BehaviorTree
    {
        public delegate void NodeEventHandler(Node sender, NodeEventArgs eventArgs);

        public event NodeEventHandler OnNodeEvent;

        Node root;

        uint time;

        List<Timer> timers = new List<Timer>();

        public void Start()
        {
            root.Start();
        }

        public void Update(uint deltaTime)
        {
            time += deltaTime;

            timers.RemoveAll(t =>
            {
                if (t.Time <= time)
                {
                    SendEvent(t.Node, new TimeoutEventArgs(t.GetHashCode()));
                    return true;
                }
                else
                    return false;
            });
        }

        void BehaviorTree_OnNodeEvent(Node sender, NodeEventArgs eventArgs)
        {
            Console.WriteLine("{0} Send {1}", sender, eventArgs);
        }

        public void SendEvent(Node sender, NodeEventArgs eventArgs)
        {
            if (OnNodeEvent != null)
                OnNodeEvent(sender, eventArgs);
        }

        public int SetTimer(Node node, uint time)
        {
            var timer = new Timer(node, time);
            timers.Add(timer);
            return timer.GetHashCode();
        }

        public Node Root
        {
            get { return root; }
            set
            {
                if (root != null)
                    throw new InvalidOperationException();

                root = value;
            }
        }
    }
}
