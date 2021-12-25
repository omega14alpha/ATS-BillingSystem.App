using System;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal class GuidGenerator : IGeneratorId
    {
        public Guid GetId(string encodedData)
        {
            if (string.IsNullOrWhiteSpace(encodedData))
            {
                throw new ArgumentException("Paremeter 'message' cannot be empty or equals null!", nameof(encodedData));
            }

            return Guid.NewGuid();
        }
    }
}
