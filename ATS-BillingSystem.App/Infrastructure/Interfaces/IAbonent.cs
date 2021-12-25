using ATS_BillingSystem.App.BillingSystem.Interfaces;

namespace ATS_BillingSystem.App.Infrastructure.Interfaces
{
    internal interface IAbonent : ISubscriber
    {
        void ConnectToPort();

        void DisconectFromPort();

        void InitiateStartCall(IPhoneNumber calledNumber);

        void InitiateStopCall();
    }
}
