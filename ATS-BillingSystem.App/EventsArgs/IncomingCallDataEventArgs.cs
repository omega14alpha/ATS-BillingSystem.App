using ATS_BillingSystem.App.BillingSystem.Interfaces;
using System;

namespace ATS_BillingSystem.App.EventsArgs
{
    internal class IncomingCallDataEventArgs : EventArgs
    {
        public IIdentifier CallId { get; set; }

        public IPhoneNumber SourceNumber { get; set; }
    }
}
