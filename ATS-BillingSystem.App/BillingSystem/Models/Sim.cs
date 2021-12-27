using ATS_BillingSystem.App.BillingSystem.Interfaces;

namespace ATS_BillingSystem.App.BillingSystem.Models
{
    internal class Sim : ISim
    {
        public IIdentifier AbonentId { get; set; }

        public IIdentifier TariffId { get; set; }

        public IPhoneNumber PhoneNumber { get; set; }
    }
}
