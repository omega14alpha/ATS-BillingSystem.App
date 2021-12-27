namespace ATS_BillingSystem.App.BillingSystem.Interfaces
{
    internal interface ITariffPlan
    {
        IIdentifier PlanId { get; set; }

        string TarrifName { get; set; }

        double PriceOfOneMinute { get; set; }
    }
}