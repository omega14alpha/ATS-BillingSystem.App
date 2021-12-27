using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.Infrastructure.Constants;
using System;
using System.Collections.Generic;

namespace ATS_BillingSystem.App.BillingSystem
{
    internal class StatisticsCollector : IStatisticsCollector
    {
        private Dictionary<IIdentifier, ICollection<IAbonentsHistory>> _historyOfAbonents;

        public IEnumerable<IAbonentsHistory> this[IIdentifier id] => _historyOfAbonents[id];

        public StatisticsCollector()
        {
            _historyOfAbonents = new Dictionary<IIdentifier, ICollection<IAbonentsHistory>>();
        }

        public void AddNewAbonentToCollection(IIdentifier abonentId)
        {
            if (abonentId == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(abonentId)));
            }

            _historyOfAbonents.Add(abonentId, new List<IAbonentsHistory>());
        }

        public void AddTestAbonentData(IIdentifier abonentId, ICollection<IAbonentsHistory> abonentHistory)
        {
            if (abonentId == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(abonentId)));
            }

            if (abonentHistory == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(abonentHistory)));
            }

            _historyOfAbonents[abonentId] = new List<IAbonentsHistory>(abonentHistory);
        }

        public void SaveNewCallData(IIdentifier abonentId, IAbonentsHistory history)
        {
            if (_historyOfAbonents.TryGetValue(abonentId, out var statisticsData))
            {
                statisticsData.Add(history);
            }
        }
    }
}
