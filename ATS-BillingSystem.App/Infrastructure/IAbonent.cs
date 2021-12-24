using ATS_BillingSystem.App.ATS;
using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Models.Abonents;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal interface IAbonent : ISubscriber, IMessager
    {
        void ConnectToPort();

        void DisconectFromPort();

        void InitiateStartCall(IPhoneNumber calledNumber);

        void InitiateStopCall();

        void ReceivePhoneSystemMessage(object sender, SystemMessageEventArgs args);
    }
}
