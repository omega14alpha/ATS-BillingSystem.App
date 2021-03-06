namespace ATS_BillingSystem.App.BillingSystem.Interfaces
{
    internal interface IContract
    {
        IIdentifier AbonentId { get; set; }

        string Name { get; set; }

        string Surname { get; set; }

        IPhoneNumber PhoneNumber { get; set; }

        ITariffPlan TariffPlan { get; set; }
    }
}
