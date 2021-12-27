using System;

namespace ATS_BillingSystem.App.BillingSystem.Interfaces
{
    internal interface IAbonentsHistory
    {
        IIdentifier CallId { get; set; }

        IPhoneNumber CalledNumber { get; set; }

        string TariffName { get; set; }

        DateTime BeginCallDateTime { get; set; }

        DateTime EndCallDateTime { get; set; }

        double Cost { get; set; }

        public double TalkTime { get; set; }
    }
}
