using System;
using System.Collections.Generic;

namespace EventDrivenBehaviorTree.Events
{
    abstract class EventObject
    {
        List<Subscription> subscriptions = new List<Subscription>();

        public virtual void Publish(EventArgs eventArgs)
        {
            foreach (var subscriber in subscriptions)
            {
                if (subscriber.EventType.IsAssignableFrom(eventArgs.GetType()))
                    subscriber.Subscriber.OnEvent(this, eventArgs);
            }
        }

        public void Subscribe(EventObject subscriber, Type eventType)
        {
            subscriptions.Add(new Subscription(subscriber, eventType));
        }

        public void Unsubscribe(EventObject subscriber, Type eventType)
        {
            subscriptions.RemoveAll(s => s.Subscriber == subscriber && s.EventType == eventType);
        }

        public void Unsubscribe(EventObject subscriber)
        {
            subscriptions.RemoveAll(s => s.Subscriber == subscriber);
        }

        protected abstract void OnEvent(EventObject publisher, EventArgs eventArgs);

        class Subscription
        {
            public readonly EventObject Subscriber;
            public readonly Type EventType;

            public Subscription(EventObject subscriber, Type eventType)
            {
                Subscriber = subscriber;
                EventType = eventType;
            }
        }
    }
}
