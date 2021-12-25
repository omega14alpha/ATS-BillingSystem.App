using ATS_BillingSystem.App.BillingSystem.Interfaces;
using System;

namespace ATS_BillingSystem.App.BillingSystem.Models
{
    internal class AbonentsHistory : IAbonentsHistory
    {
        public IPhoneNumber CalledNumber { get; set; }

        public DateTime BeginCallDateTime { get; set; }

        public DateTime EndCallDateTime { get; set; }

        public double TalkTime => 
            (EndCallDateTime - BeginCallDateTime).TotalMinutes;
    }
}
