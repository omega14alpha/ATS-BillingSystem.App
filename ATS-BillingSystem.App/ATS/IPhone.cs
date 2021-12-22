using ATS_BillingSystem.App.EventsArgs;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal interface IPhone : ITerminal
    {
        event EventHandler<EventArgs> OnConnectToPort;

        event EventHandler<EventArgs> OnDisconnectFromPort;

        event EventHandler<CallDataEventArgs> OnPhoneStartCall;

        event EventHandler<CallDataEventArgs> OnPhoneStopCall;

        event EventHandler<SystemMessageEventArgs> OnSendPhoneSystemMessage;
    }
}
