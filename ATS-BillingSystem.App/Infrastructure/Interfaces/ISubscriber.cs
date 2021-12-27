using ATS_BillingSystem.App.BillingSystem.Interfaces;

namespace ATS_BillingSystem.App.Infrastructure.Interfaces
{
    internal interface ISubscriber
    {
        IContract Contract { get; }
    }
}
