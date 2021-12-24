using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Models.Abonents;

namespace ATS_BillingSystem.App.ATS
{
    internal interface ITerminal
    {
        void ConnectToPort();

        void DisconnectFromPort();

        void StartCall(IPhoneNumber calledNumber);

        void StopCall();

        void AcceptIncomingCallFromPort(object sender, CallDataEventArgs args);

        void AcceptIncomingEndCallFromPort(object sender, CallDataEventArgs args);        
    }
}
