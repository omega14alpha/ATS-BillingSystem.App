using ATS_BillingSystem.App.BillingSystem.Interfaces;
using System;

namespace ATS_BillingSystem.App.BillingSystem.Models
{
    internal class AbonentId : IIdentifier
    {
        public Guid Id { get; set; }
    }
}
