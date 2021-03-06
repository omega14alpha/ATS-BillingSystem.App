using ATS_BillingSystem.App.BillingSystem.Interfaces;

namespace ATS_BillingSystem.App.BillingSystem.Models
{
    internal class AbonentContract : IContract
    {
        public IIdentifier AbonentId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public IPhoneNumber PhoneNumber { get; set; }

        public ITariffPlan TariffPlan { get; set; }
    }
}
