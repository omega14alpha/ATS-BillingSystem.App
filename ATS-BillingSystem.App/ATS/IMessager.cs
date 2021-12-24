using ATS_BillingSystem.App.EventsArgs;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal interface IMessager
    {
        event EventHandler<SystemMessageEventArgs> OnSendSystemMessage;
    }
}
