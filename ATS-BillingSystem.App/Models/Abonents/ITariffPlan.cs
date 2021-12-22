namespace ATS_BillingSystem.App.Models.Abonents
{
    public interface ITariffPlan
    {
        int PlanId { get; set; }

        string TarrifName { get; set; }

        double PriceOfOneMinute { get; set; }
    }
}