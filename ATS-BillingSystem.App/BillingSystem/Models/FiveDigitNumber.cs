using ATS_BillingSystem.App.BillingSystem.Interfaces;

namespace ATS_BillingSystem.App.BillingSystem.Models
{
    internal class FiveDigitNumber : IPhoneNumber
    {
        public string Number { get; set; }
    }
}
