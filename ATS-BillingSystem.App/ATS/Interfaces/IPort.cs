using ATS_BillingSystem.App.ATS.Models;
using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.EventsArgs;
using System;

namespace ATS_BillingSystem.App.ATS.Interfaces
{
    internal interface IPort : IMessager
    {
        PortState State { get; }

        Func<object, OutgoingCallDataEventArgs, IIdentifier> OnPortStartCall { get; set; }

        event EventHandler<OutgoingCallDataEventArgs> OnPortStopCall;

        public event EventHandler<IncomingCallDataEventArgs> OnPortStartIncomingCall;

        public event EventHandler<IncomingCallDataEventArgs> OnPortStopIncomingCall;

        void ConnectTerminalToPort(object sender, EventArgs args);

        void DisconnectTerminalFromPort(object sender, EventArgs args);

        void PortStartCall(object sender, OutgoingCallDataEventArgs args);

        void PortStopCall(object sender, OutgoingCallDataEventArgs args);

        void PortStartIncomingCall(object sender, IncomingCallDataEventArgs args);

        void PortStopIncomingCall(object sender, IncomingCallDataEventArgs args);

        void PortAcceptIncomingCall(object sender, IncomingCallDataEventArgs args);
    }
}
