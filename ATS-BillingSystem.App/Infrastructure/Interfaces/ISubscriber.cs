using ATS_BillingSystem.App.BillingSystem.Interfaces;
using System;
using System.Collections.Generic;

namespace ATS_BillingSystem.App.Infrastructure.Interfaces
{
    internal interface ISubscriber
    {
        IContract Contract { get; }

        IEnumerable<IAbonentsHistory> GetStatistic(IStatisticsCollector statisticsCollector, Func<IAbonentsHistory, bool> func);
    }
}
