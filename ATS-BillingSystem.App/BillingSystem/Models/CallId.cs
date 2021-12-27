using ATS_BillingSystem.App.BillingSystem.Interfaces;
using System;

namespace ATS_BillingSystem.App.BillingSystem.Models
{
    internal class CallId : IIdentifier
    {
        public Guid Id { get ; set; }
    }
}
