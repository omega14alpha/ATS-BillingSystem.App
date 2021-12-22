namespace ATS_BillingSystem.App.Models.Abonents
{
    internal class TariffPlanLight : ITariffPlan
    {
        public int PlanId { get; set; }

        public string TarrifName { get; set; }

        public double PriceOfOneMinute { get; set; }
    }
}
