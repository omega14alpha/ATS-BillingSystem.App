using System;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal interface IGeneratorId
    {
        Guid GetId(string encodedData);
    }
}
