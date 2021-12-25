using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.EventsArgs;
using System;

namespace ATS_BillingSystem.App.ATS.Interfaces
{
    internal interface ITerminal : IMessager
    {
        event EventHandler<EventArgs> OnConnectToPort;

        event EventHandler<EventArgs> OnDisconnectFromPort;

        event EventHandler<CallDataEventArgs> OnPhoneStartCall;

        event EventHandler<CallDataEventArgs> OnPhoneStopCall;

        void ConnectToPort();

        void DisconnectFromPort();

        void StartCall(IPhoneNumber calledNumber);

        void StopCall();

        void AcceptIncomingCallFromPort(object sender, CallDataEventArgs args);

        void AcceptIncomingEndCallFromPort(object sender, CallDataEventArgs args);
    }
}
