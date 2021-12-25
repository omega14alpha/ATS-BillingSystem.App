using ATS_BillingSystem.App.EventsArgs;
using System;

namespace ATS_BillingSystem.App.ATS.Interfaces
{
    internal interface IMessager
    {
        event EventHandler<SystemMessageEventArgs> OnSendSystemMessage;

        void ReceivingIncomingMessages(object sender, SystemMessageEventArgs args);
    }
}
