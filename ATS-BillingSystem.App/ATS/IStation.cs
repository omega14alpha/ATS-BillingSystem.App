using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Models.Systems;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal interface IStation
    {
        event EventHandler<HistoryEventArgs> OnRecordingCallStartData;

        event EventHandler<HistoryEventArgs> OnRecordingCallEndData;

        event EventHandler<SystemMessageEventArgs> OnSendStationSystemMessage;

        bool ReceiveIncomingCallFromPort(object sender, CallDataEventArgs args);

        void ReceiveEndCurrentCallFromPort(object sender, CallDataEventArgs args);

        void PortStateChanged(object sender, PortStateEventArgs args);
    }
}
