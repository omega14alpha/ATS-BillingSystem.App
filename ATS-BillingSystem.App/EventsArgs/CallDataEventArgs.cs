using ATS_BillingSystem.App.Models.Abonents;
using System;

namespace ATS_BillingSystem.App.EventsArgs
{
    internal class CallDataEventArgs : EventArgs
    {
        public IAbonenId AbonentId { get; set; }

        public IPhoneNumber SourceNumber { get; set; }

        public IPhoneNumber CalledNumber { get; set; }
    }
}
