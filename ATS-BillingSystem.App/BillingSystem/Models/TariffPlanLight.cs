using ATS_BillingSystem.App.BillingSystem.Interfaces;

namespace ATS_BillingSystem.App.BillingSystem.Models
{
    internal class TariffPlanLight : ITariffPlan
    {
        public int PlanId { get; set; }

        public string TarrifName { get; set; }

        public double PriceOfOneMinute { get; set; }
    }
}
