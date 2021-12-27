using ATS_BillingSystem.App.BillingSystem.Interfaces;
using System;

namespace ATS_BillingSystem.App.EventsArgs
{
    internal class OutgoingCallDataEventArgs : EventArgs
    {
        public IIdentifier CallId { get; set; }

        public ISim AbonentData { get; set; }

        public IPhoneNumber CalledNumber { get; set; }
    }
}
