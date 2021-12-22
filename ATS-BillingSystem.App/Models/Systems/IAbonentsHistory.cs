using ATS_BillingSystem.App.Models.Abonents;
using System;

namespace ATS_BillingSystem.App.Models.Systems
{
    internal interface IAbonentsHistory
    {
        IPhoneNumber TargetNumber { get; set; }

        DateTime BeginCallDateTime { get; set; }

        DateTime EndCallDateTime { get; set; }

        public double TalkTime { get; }
    }
}
