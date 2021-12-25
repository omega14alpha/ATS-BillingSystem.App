using ATS_BillingSystem.App.BillingSystem.Interfaces;

namespace ATS_BillingSystem.App.Infrastructure.Interfaces
{
    internal interface IClientManager
    {
        IAbonent RegisterNewAbonent(string name, string surname, ITariffPlan tariffPlan);
    }
}
