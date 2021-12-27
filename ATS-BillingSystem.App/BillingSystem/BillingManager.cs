using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.BillingSystem.Models;
using ATS_BillingSystem.App.EventsArgs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATS_BillingSystem.App.BillingSystem
{
    internal class BillingManager : IBillingManager
    {
        private IList<IAbonentsHistory> _currentCalls;

        private IStatisticsCollector _statistics;

        private ITariffController _tariffController;

        public BillingManager(IStatisticsCollector statisticsCollector, ITariffController tariffController)
        {
            _statistics = statisticsCollector;
            _tariffController = tariffController;
            _currentCalls = new List<IAbonentsHistory>();
        }

        public void SaveNewCallStartData(object sender, OutgoingCallDataEventArgs args)
        {
            string tariffName = _tariffController.Tariffs.FirstOrDefault(s => s.PlanId == args.AbonentData.TariffId).TarrifName;
            IAbonentsHistory history = new AbonentsHistory()
            {
                CallId = args.CallId,
                CalledNumber = args.CalledNumber,
                BeginCallDateTime = DateTime.Now,
                TariffName = tariffName
            };

            _currentCalls.Add(history);
        }

        public void SaveNewCallEndData(object sender, OutgoingCallDataEventArgs args)
        {
            var history = _currentCalls.FirstOrDefault(s => s.CallId == args.CallId);
            double price = _tariffController.Tariffs.FirstOrDefault(s => s.PlanId == args.AbonentData.TariffId).PriceOfOneMinute;
            history.EndCallDateTime = DateTime.Now;
            history.TalkTime = (history.EndCallDateTime - history.BeginCallDateTime).TotalMinutes;
            history.Cost = history.TalkTime * price;

            _statistics.SaveNewCallData(args.AbonentData.AbonentId, history);
            _currentCalls.Remove(history);
        }

        public IEnumerable<IAbonentsHistory> GetCallsStatistic(IIdentifier abonentId, Func<IAbonentsHistory, bool> func) =>
            _statistics[abonentId].Where(func);

        public IEnumerable<IAbonentsHistory> GetCallsStatistic(IIdentifier abonentId) =>
            _statistics[abonentId];
    }
}
