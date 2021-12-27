using System.Collections.Generic;

namespace ATS_BillingSystem.App.BillingSystem.Interfaces
{
    internal interface ITariffController
    {
        IEnumerable<ITariffPlan> Tariffs { get; }

        void AddNewTariffPlan(ITariffPlan tariff);
    }
}
