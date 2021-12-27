using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.EventsArgs;
using System;

namespace ATS_BillingSystem.App.ATS.Interfaces
{
    internal interface IStation : IMessager
    {
        event EventHandler<OutgoingCallDataEventArgs> OnRecordingCallStartData;

        event EventHandler<OutgoingCallDataEventArgs> OnRecordingCallEndData;

        event EventHandler<IncomingCallDataEventArgs> OnIncomingCallEnd;

        IIdentifier ReceiveIncomingCallFromPort(object sender, OutgoingCallDataEventArgs args);

        void ReceiveEndCurrentCallFromPort(object sender, OutgoingCallDataEventArgs args);
    }
}
