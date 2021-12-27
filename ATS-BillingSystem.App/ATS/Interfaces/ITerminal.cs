using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.EventsArgs;
using System;

namespace ATS_BillingSystem.App.ATS.Interfaces
{
    internal interface ITerminal : IMessager
    {
        event EventHandler<EventArgs> OnConnectToPort;

        event EventHandler<EventArgs> OnDisconnectFromPort;

        event EventHandler<OutgoingCallDataEventArgs> OnPhoneStartCall;

        event EventHandler<OutgoingCallDataEventArgs> OnPhoneStopCall;

        void ConnectToPort();

        void DisconnectFromPort();

        void StartCall(IPhoneNumber calledNumber);

        void StopCall();

        void AcceptIncomingCall();

        void RejectIncomingCall();

        void AcceptIncomingCallFromPort(object sender, IncomingCallDataEventArgs args);

        void AcceptIncomingEndCallFromPort(object sender, IncomingCallDataEventArgs args);
    }
}
