namespace ATS_BillingSystem.App.BillingSystem.Interfaces
{
    public interface ITariffPlan
    {
        int PlanId { get; set; }

        string TarrifName { get; set; }

        double PriceOfOneMinute { get; set; }
    }
}