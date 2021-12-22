using ATS_BillingSystem.App.Models.Abonents;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal interface IClientManager
    {
        IAbonent RegisterNewAbonent(string name, string surname, ITariffPlan tariffPlan);
    }
}
