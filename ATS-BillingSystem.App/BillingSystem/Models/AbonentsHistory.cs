using ATS_BillingSystem.App.BillingSystem.Interfaces;
using System;

namespace ATS_BillingSystem.App.BillingSystem.Models
{
    internal class AbonentsHistory : IAbonentsHistory
    {
        public IIdentifier CallId { get; set; }

        public IPhoneNumber CalledNumber { get; set; }

        public string TariffName { get; set; }

        public DateTime BeginCallDateTime { get; set; }

        public DateTime EndCallDateTime { get; set; }

        public double Cost { get; set; }

        public double TalkTime { get; set; }
    }
}
