using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Models.Abonents;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal interface IIntermediateDevice
    {
        void ConnectTerminalToPort(object sender, EventArgs args);

        void DisconnectTerminalFromPort(object sender, EventArgs args);

        void PortStartCall(object sender, CallDataEventArgs args);

        void PortStopCall(object sender, CallDataEventArgs args);

        void PortStartIncomingCall(IPhoneNumber sourceNumber);

        void PortStopIncomingCall(IPhoneNumber sourceNumber);
    }
}
