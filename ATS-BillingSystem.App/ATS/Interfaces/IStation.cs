using ATS_BillingSystem.App.EventsArgs;
using System;

namespace ATS_BillingSystem.App.ATS.Interfaces
{
    internal interface IStation : IMessager
    {
        event EventHandler<HistoryEventArgs> OnRecordingCallStartData;

        event EventHandler<HistoryEventArgs> OnRecordingCallEndData;

        bool ReceiveIncomingCallFromPort(object sender, CallDataEventArgs args);

        void ReceiveEndCurrentCallFromPort(object sender, CallDataEventArgs args);

        void PortStateChanged(object sender, PortStateEventArgs args);
    }
}
