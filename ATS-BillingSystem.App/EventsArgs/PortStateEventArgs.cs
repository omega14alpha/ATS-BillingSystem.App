using ATS_BillingSystem.App.Models.Systems;
using System;

namespace ATS_BillingSystem.App.EventsArgs
{
    internal class PortStateEventArgs : EventArgs
    {
        public PortState NewState { get; set; }
    }
}
