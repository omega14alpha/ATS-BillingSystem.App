using ATS_BillingSystem.App.BillingSystem.Interfaces;
using System;

namespace ATS_BillingSystem.App.EventsArgs
{
    internal class HistoryEventArgs : EventArgs
    {
        public IIdentifier CallId { get; set; }

        public ISim Sim { get; set; }

        public DateTime DateTime { get; set; }
    }
}
