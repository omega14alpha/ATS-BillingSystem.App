using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Models.Abonents;
using System;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal interface IAbonent : ISubscriber
    {
        event EventHandler<SystemMessageEventArgs> OnSendAbonentSystemMessage;

        void ConnectToPort();

        void DisconectFromPort();

        void InitiateStartCall(IPhoneNumber calledNumber);

        void InitiateStopCall();

        void ReceivePhoneSystemMessage(object sender, SystemMessageEventArgs args);
    }
}
