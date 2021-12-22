namespace ATS_BillingSystem.App.Infrastructure
{
    internal interface IGeneratorId
    {
        string GetId(string encodedData);
    }
}
