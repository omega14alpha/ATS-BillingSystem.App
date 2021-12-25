using ATS_BillingSystem.App.Infrastructure.Interfaces;
using System;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal class FiveDigitNumberGenerator : IPhoneNumberGenerator
    {
        private const string pattern = "##-#-##";

        public string GetPhoneNumber()
        {
            Random random = new Random();
            float number = random.Next(10000, 100000);
            return number.ToString(pattern);
        }
    }
}
