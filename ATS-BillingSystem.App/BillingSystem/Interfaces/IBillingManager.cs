using ATS_BillingSystem.App.EventsArgs;
using System;
using System.Collections.Generic;

namespace ATS_BillingSystem.App.BillingSystem.Interfaces
{
    internal interface IBillingManager
    {
        void SaveNewCallStartData(object sender, OutgoingCallDataEventArgs args);

        void SaveNewCallEndData(object sender, OutgoingCallDataEventArgs args);

        IEnumerable<IAbonentsHistory> GetCallsStatistic(IIdentifier abonentId, Func<IAbonentsHistory, bool> func);

        IEnumerable<IAbonentsHistory> GetCallsStatistic(IIdentifier abonentId);
    }
}
