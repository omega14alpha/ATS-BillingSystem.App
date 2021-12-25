using ATS_BillingSystem.App.Infrastructure.Interfaces;
using System;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal class GuidGenerator : IGeneratorId
    {
        public Guid GetId() => Guid.NewGuid();        
    }
}
