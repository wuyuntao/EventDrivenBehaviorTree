using System;
using System.Collections.Generic;

namespace EventDrivenBehaviorTree.Events
{
    public class EventBus
    {
        List<Subscription> subscriptions = new List<Subscription>();

        public virtual void Publish(IPublisher publisher, EventArgs eventArgs)
        {
            foreach (var subscriber in subscriptions)
            {
                if (subscriber.EventType.IsAssignableFrom(eventArgs.GetType()))
                    subscriber.Subscriber.OnEvent(publisher, eventArgs);
            }
        }

        public void Subscribe(ISubscriber subscriber, Type eventType)
        {
            subscriptions.Add(new Subscription(subscriber, eventType));
        }

        public void Unsubscribe(ISubscriber subscriber, Type eventType)
        {
            subscriptions.RemoveAll(s => s.Subscriber == subscriber && s.EventType == eventType);
        }

        public void Unsubscribe(ISubscriber subscriber)
        {
            subscriptions.RemoveAll(s => s.Subscriber == subscriber);
        }

        public interface IPublisher
        {
        }

        public interface ISubscriber
        {
            void OnEvent(IPublisher publisher, EventArgs eventArgs);
        }

        class Subscription
        {
            public readonly ISubscriber Subscriber;
            public readonly Type EventType;

            public Subscription(ISubscriber subscriber, Type eventType)
            {
                Subscriber = subscriber;
                EventType = eventType;
            }
        }
    }
}