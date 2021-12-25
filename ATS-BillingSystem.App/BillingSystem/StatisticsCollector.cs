using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.BillingSystem.Models;
using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATS_BillingSystem.App.BillingSystem
{
    internal class StatisticsCollector : IStatisticsCollector
    {
        private Dictionary<IAbonenId, ICollection<IAbonentsHistory>> _abonents;

        public StatisticsCollector()
        {
            _abonents = new Dictionary<IAbonenId, ICollection<IAbonentsHistory>>();
        }

        public void AddNewAbonentToCollection(IAbonenId abonentId)
        {
            if (abonentId == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(abonentId)));
            }

            _abonents.Add(abonentId, new List<IAbonentsHistory>());
        }

        public void AddTestAbonentData(IAbonenId abonentId, ICollection<IAbonentsHistory> abonentHistory)
        {
            if (abonentId == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(abonentId)));
            }

            if (abonentHistory == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(abonentHistory)));
            }

            _abonents[abonentId] = new List<IAbonentsHistory>(abonentHistory);
        }

        public void SaveNewCallStartData(object sender, HistoryEventArgs args)
        {
            if (_abonents.TryGetValue(args.AbonentId, out var abonent))
            {
                var newRecord = new AbonentsHistory()
                {
                    CalledNumber = args.CalledNumber,
                    BeginCallDateTime = args.DateTime
                };

                abonent.Add(newRecord);
            }
        }

        public void SaveNewCallEndData(object sender, HistoryEventArgs args)
        {
            if (_abonents.TryGetValue(args.AbonentId, out var abonent))
            {
                var abonentLastRecord = abonent.LastOrDefault();
                if (abonentLastRecord != null)
                {
                    abonentLastRecord.EndCallDateTime = args.DateTime;
                }
            }
        }

        public IEnumerable<IAbonentsHistory> GetAbonentStatistic(IAbonenId abonentId, Func<IAbonentsHistory, bool> func)
        {
            if (abonentId == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(abonentId)));
            }

            if (_abonents.TryGetValue(abonentId, out var abonent))
            {
                return abonent.Where(func);
            }

            return null;
        }
    }
}
