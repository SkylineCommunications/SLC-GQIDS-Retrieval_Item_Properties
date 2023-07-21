namespace PropertyRetrieval.Connection
{
    using System;
    using Skyline.DataMiner.Analytics.GenericInterface;
    using Skyline.DataMiner.Net;
    using Skyline.DataMiner.Net.Async;
    using Skyline.DataMiner.Net.Messages;

    internal class GQIConnection : IConnection
    {
        private GQIDMS _dms;

        public GQIConnection(GQIDMS dms)
        {
            _dms = dms;
        }

        public event ConnectionClosedHandler OnClose;

        public event NewMessageEventHandler OnNewMessage;

        public event AbnormalCloseEventHandler OnAbnormalClose;

        public event EventsDroppedEventHandler OnEventsDropped;

        public event SubscriptionCompleteEventHandler OnSubscriptionComplete;

        public event AuthenticationChallengeEventHandler OnAuthenticationChallenge;

        public event EventHandler<SubscriptionStateEventArgs> OnSubscriptionState;

        public string UserDomainName => throw new NotImplementedException();

        public Guid ConnectionID => throw new NotImplementedException();

        public bool IsShuttingDown => throw new NotImplementedException();

        public IAsyncMessageHandler Async => throw new NotImplementedException();

        public bool IsReceiving => throw new NotImplementedException();

        public ServerDetails ServerDetails => throw new NotImplementedException();

        public void AddSubscription(string setID, params SubscriptionFilter[] newFilters)
        {
            throw new NotImplementedException();
        }

        public void ClearSubscriptions(string setID)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void FireOnAsyncResponse(AsyncResponseEvent responseEvent, ref bool handled)
        {
            throw new NotImplementedException();
        }

        public GetElementProtocolResponseMessage GetElementProtocol(int dmaid, int eid)
        {
            throw new NotImplementedException();
        }

        public GetProtocolInfoResponseMessage GetProtocol(string name, string version)
        {
            throw new NotImplementedException();
        }

        public DMSMessage[] HandleMessage(DMSMessage msg)
        {
            return _dms.SendMessages(new DMSMessage[] { msg });
        }

        public DMSMessage[] HandleMessages(DMSMessage[] msgs)
        {
            return _dms.SendMessages(msgs);
        }

        public DMSMessage HandleSingleResponseMessage(DMSMessage msg)
        {
            return _dms.SendMessage(msg);
        }

        public void RemoveSubscription(string setID, params SubscriptionFilter[] deletedFilters)
        {
            throw new NotImplementedException();
        }

        public void ReplaceSubscription(string setID, params SubscriptionFilter[] newFilters)
        {
            throw new NotImplementedException();
        }

        public void SafeWait(int timeout)
        {
            throw new NotImplementedException();
        }

        public CreateSubscriptionResponseMessage Subscribe(params SubscriptionFilter[] filters)
        {
            throw new NotImplementedException();
        }

        public bool SupportsFeature(CompatibilityFlags flags)
        {
            throw new NotImplementedException();
        }

        public bool SupportsFeature(string name)
        {
            throw new NotImplementedException();
        }

        public ITrackedSubscriptionUpdate TrackAddSubscription(string setID, params SubscriptionFilter[] newFilters)
        {
            throw new NotImplementedException();
        }

        public ITrackedSubscriptionUpdate TrackClearSubscriptions(string setID)
        {
            throw new NotImplementedException();
        }

        public ITrackedSubscriptionUpdate TrackRemoveSubscription(string setID, params SubscriptionFilter[] deletedFilters)
        {
            throw new NotImplementedException();
        }

        public ITrackedSubscriptionUpdate TrackReplaceSubscription(string setID, params SubscriptionFilter[] newFilters)
        {
            throw new NotImplementedException();
        }

        public ITrackedSubscriptionUpdate TrackSubscribe(params SubscriptionFilter[] filters)
        {
            throw new NotImplementedException();
        }

        public ITrackedSubscriptionUpdate TrackUpdateSubscription(UpdateSubscriptionMultiMessage multi)
        {
            throw new NotImplementedException();
        }

        public DMSMessage[] UnPack(DMSMessage[] messages)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe()
        {
            throw new NotImplementedException();
        }
    }
}
