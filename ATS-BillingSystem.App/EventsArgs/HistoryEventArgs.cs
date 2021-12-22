using ATS_BillingSystem.App.Models.Abonents;
using System;

namespace ATS_BillingSystem.App.EventsArgs
{
    internal class HistoryEventArgs : EventArgs
    {
        public IAbonenId AbonentId { get; set; }

        public IPhoneNumber TargetNumber { get; set; }

        public DateTime DateTime { get; set; }
    }
}
