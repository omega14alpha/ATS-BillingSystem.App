namespace ATS_BillingSystem.App.BillingSystem.Interfaces
{
    internal interface ISim
    {
        IIdentifier AbonentId { get; set; }

        IIdentifier TariffId { get; set; }

        IPhoneNumber PhoneNumber { get; set; }
    }
}
