using ATS_BillingSystem.App.Models.Abonents;
using System;

namespace ATS_BillingSystem.App.Models.Systems
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
