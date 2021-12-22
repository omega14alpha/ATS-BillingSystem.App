using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Models.Abonents;
using ATS_BillingSystem.App.Models.Systems;
using System;
using System.Collections.Generic;

namespace ATS_BillingSystem.App.BillingSystem
{
    internal interface IStatisticsCollector
    {
        void AddNewAbonentToCollection(IAbonenId abonentId);

        void AddTestAbonentData(IAbonenId abonentId, ICollection<IAbonentsHistory> abonentHistory);

        void SaveNewCallStartData(object sender, HistoryEventArgs args);

        void SaveNewCallEndData(object sender, HistoryEventArgs args);

        IEnumerable<IAbonentsHistory> GetAbonentStatistic(IAbonenId abonentId, Func<IAbonentsHistory, bool> func);
    }
}
