using ATS_BillingSystem.App.Models.Abonents;
using System;

namespace ATS_BillingSystem.App.BillingSystem.Interfaces
{
    internal interface IAbonentsHistory
    {
        IPhoneNumber CalledNumber { get; set; }

        DateTime BeginCallDateTime { get; set; }

        DateTime EndCallDateTime { get; set; }

        public double TalkTime { get; }
    }
}
