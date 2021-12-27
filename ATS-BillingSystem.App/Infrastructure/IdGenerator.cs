using System;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal static class IdGenerator
    {
        public static Guid GetId() => Guid.NewGuid();        
    }
}
