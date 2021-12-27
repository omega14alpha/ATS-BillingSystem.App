using ATS_BillingSystem.App.BillingSystem.Interfaces;
using System.Collections.Generic;

namespace ATS_BillingSystem.App.BillingSystem
{
    internal class TariffController : ITariffController
    {
        private ICollection<ITariffPlan> _tariffs;

        public IEnumerable<ITariffPlan> Tariffs => _tariffs;

        public TariffController()
        {
            _tariffs = new List<ITariffPlan>();
        }

        public void AddNewTariffPlan(ITariffPlan tariff)
        {
            _tariffs.Add(tariff);
        }
    }
}
