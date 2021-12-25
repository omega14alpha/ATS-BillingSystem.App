using ATS_BillingSystem.App.ATS.Models;
using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.EventsArgs;
using System;

namespace ATS_BillingSystem.App.ATS.Interfaces
{
    internal interface IPort : IMessager
    {
        IPhoneNumber Number { get; }

        PortState State { get; set; }

        Func<object, CallDataEventArgs, bool> OnPortStartCall { get; set; }

        event EventHandler<PortStateEventArgs> OnPortStateChange;

        event EventHandler<CallDataEventArgs> OnPortStopCall;

        event EventHandler<CallDataEventArgs> OnPortStartIncomingCall;

        event EventHandler<CallDataEventArgs> OnPortStopIncomingCall;

        void ConnectTerminalToPort(object sender, EventArgs args);

        void DisconnectTerminalFromPort(object sender, EventArgs args);

        void PortStartCall(object sender, CallDataEventArgs args);

        void PortStopCall(object sender, CallDataEventArgs args);

        void PortStartIncomingCall(IPhoneNumber sourceNumber);

        void PortStopIncomingCall(IPhoneNumber sourceNumber);
    }
}
