using ATS_BillingSystem.App.EventsArgs;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal interface IPhone : ITerminal
    {
        event EventHandler<EventArgs> OnConnectToPort;

        event EventHandler<EventArgs> OnDisconnectFromPort;

        event EventHandler<CallDataEventArgs> OnTerminalStartCall;

        event EventHandler<CallDataEventArgs> OnTerminalStopCall;

        event EventHandler<SystemMessageEventArgs> OnSendTerminalSystemMessage;
    }
}
