using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conferences.UI
{
    public static class Messages
    {
        public const string ConferenceEditied = "ConferenceEditied";
    }
    public static class MessagingCenter
    {
        

        private static Dictionary<Tuple<string, Type, Type>, List<Tuple<WeakReference, Action<object, object>>>> callbacks = new Dictionary<Tuple<string, Type, Type>, List<Tuple<WeakReference, Action<object, object>>>>();

        public static void Subscribe<TSender, TArgs>(object subscriber, string message, Action<TSender, TArgs> callback, TSender source = null) where TSender : class
        {
            if (subscriber == null)
                throw new ArgumentNullException("subscriber");
            if (callback == null)
                throw new ArgumentNullException("callback");
            Action<object, object> callback1 = (Action<object, object>)((sender, args) =>
            {
                if ((object)(TSender)source != null && (object)(TSender)sender != (object)(TSender)source)
                    return;
                callback((TSender)sender, (TArgs)args);
            });
            MessagingCenter.InnerSubscribe(subscriber, message, typeof(TSender), typeof(TArgs), callback1);
        }

        public static void Subscribe<TSender>(object subscriber, string message, Action<TSender> callback, TSender source = null) where TSender : class
        {
            if (subscriber == null)
                throw new ArgumentNullException("subscriber");
            if (callback == null)
                throw new ArgumentNullException("callback");
            Action<object, object> callback1 = (Action<object, object>)((sender, args) =>
            {
                if ((object)(TSender)source != null && (object)(TSender)sender != (object)(TSender)source)
                    return;
                callback((TSender)sender);
            });
            MessagingCenter.InnerSubscribe(subscriber, message, typeof(TSender), (Type)null, callback1);
        }

        public static void Unsubscribe<TSender, TArgs>(object subscriber, string message) where TSender : class
        {
            MessagingCenter.InnerUnsubscribe(message, typeof(TSender), typeof(TArgs), subscriber);
        }

        public static void Unsubscribe<TSender>(object subscriber, string message) where TSender : class
        {
            MessagingCenter.InnerUnsubscribe(message, typeof(TSender), (Type)null, subscriber);
        }

        public static void Send<TSender, TArgs>(TSender sender, string message, TArgs args) where TSender : class
        {
            if ((object)sender == null)
                throw new ArgumentNullException("sender");
            MessagingCenter.InnerSend(message, typeof(TSender), typeof(TArgs), (object)sender, (object)args);
        }

        public static void Send<TSender>(TSender sender, string message) where TSender : class
        {
            if ((object)sender == null)
                throw new ArgumentNullException("sender");
            MessagingCenter.InnerSend(message, typeof(TSender), (Type)null, (object)sender, (object)null);
        }

        internal static void ClearSubscribers()
        {
            MessagingCenter.callbacks.Clear();
        }

        private static void InnerUnsubscribe(string message, Type senderType, Type argType, object subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException("subscriber");
            if (message == null)
                throw new ArgumentNullException("message");
            Tuple<string, Type, Type> key = new Tuple<string, Type, Type>(message, senderType, argType);
            if (!MessagingCenter.callbacks.ContainsKey(key))
                return;
            MessagingCenter.callbacks[key].RemoveAll((Predicate<Tuple<WeakReference, Action<object, object>>>)(tuple =>
            {
                if (tuple.Item1.IsAlive)
                    return tuple.Item1.Target == subscriber;
                return true;
            }));
            if (Enumerable.Any<Tuple<WeakReference, Action<object, object>>>((IEnumerable<Tuple<WeakReference, Action<object, object>>>)MessagingCenter.callbacks[key]))
                return;
            MessagingCenter.callbacks.Remove(key);
        }

        private static void InnerSubscribe(object subscriber, string message, Type senderType, Type argType, Action<object, object> callback)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            Tuple<string, Type, Type> key = new Tuple<string, Type, Type>(message, senderType, argType);
            Tuple<WeakReference, Action<object, object>> tuple = new Tuple<WeakReference, Action<object, object>>(new WeakReference(subscriber), callback);
            if (MessagingCenter.callbacks.ContainsKey(key))
            {
                MessagingCenter.callbacks[key].Add(tuple);
            }
            else
            {
                List<Tuple<WeakReference, Action<object, object>>> list = new List<Tuple<WeakReference, Action<object, object>>>()
        {
          tuple
        };
                MessagingCenter.callbacks[key] = list;
            }
        }

        private static void InnerSend(string message, Type senderType, Type argType, object sender, object args)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            Tuple<string, Type, Type> key = new Tuple<string, Type, Type>(message, senderType, argType);
            if (!MessagingCenter.callbacks.ContainsKey(key))
                return;
            List<Tuple<WeakReference, Action<object, object>>> list = MessagingCenter.callbacks[key];
            if (list == null || !Enumerable.Any<Tuple<WeakReference, Action<object, object>>>((IEnumerable<Tuple<WeakReference, Action<object, object>>>)list))
                return;
            foreach (Tuple<WeakReference, Action<object, object>> tuple in list)
            {
                if (tuple.Item1.IsAlive)
                    tuple.Item2(sender, args);
            }
        }
    }
}
