using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Models.Abonents;
using ATS_BillingSystem.App.Models.Systems;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal interface IPort : IIntermediateDevice
    {
        IPhoneNumber Number { get; }

        PortState State { get; set; }

        Func<object, CallDataEventArgs, bool> OnPortStartCall { get; set; }

        event EventHandler<PortStateEventArgs> OnPortStateChange;

        event EventHandler<CallDataEventArgs> OnPortStopCall;

        event EventHandler<CallDataEventArgs> OnPortStartIncomingCall;

        event EventHandler<CallDataEventArgs> OnPortStopIncomingCall;

        event EventHandler<SystemMessageEventArgs> OnSendPortSystemMessage;
    }
}
