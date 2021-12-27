using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.EventsArgs;
using System;

namespace ATS_BillingSystem.App.Infrastructure.Interfaces
{
    internal interface IAbonent : ISubscriber
    {
        event EventHandler<SystemMessageEventArgs> OnSendSystemMessage;

        void ConnectToPort();

        void DisconectFromPort();

        void InitiateStartCall(IPhoneNumber calledNumber);

        void InitiateStopCall();

        void AcceptIncomingCall();

        void RejectIncomingCall();
    }
}
