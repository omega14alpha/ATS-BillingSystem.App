using ATS_BillingSystem.App.EventsArgs;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal interface IPhone : ITerminal, IMessager
    {
        event EventHandler<EventArgs> OnConnectToPort;

        event EventHandler<EventArgs> OnDisconnectFromPort;

        event EventHandler<CallDataEventArgs> OnPhoneStartCall;

        event EventHandler<CallDataEventArgs> OnPhoneStopCall;
    }
}
