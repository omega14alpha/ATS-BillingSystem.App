using ATS_BillingSystem.App.ATS.Models;
using System;

namespace ATS_BillingSystem.App.EventsArgs
{
    internal class PortStateEventArgs : EventArgs
    {
        public PortState NewState { get; set; }
    }
}
