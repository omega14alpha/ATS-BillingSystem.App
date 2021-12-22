namespace ATS_BillingSystem.App.Models.Abonents
{
    internal class AbonentContract : IContract
    {
        public IAbonenId AbonentId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public IPhoneNumber PhoneNumber { get; set; }

        public ITariffPlan TariffPlan { get; set; }
    }
}
