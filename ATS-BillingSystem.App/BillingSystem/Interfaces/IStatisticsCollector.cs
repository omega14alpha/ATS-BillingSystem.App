using System.Collections.Generic;

namespace ATS_BillingSystem.App.BillingSystem.Interfaces
{
    internal interface IStatisticsCollector
    {
        IEnumerable<IAbonentsHistory> this[IIdentifier id] { get; }

        void AddNewAbonentToCollection(IIdentifier abonentId);

        void AddTestAbonentData(IIdentifier abonentId, ICollection<IAbonentsHistory> abonentHistory);

        void SaveNewCallData(IIdentifier abonentId, IAbonentsHistory history);
    }
}
