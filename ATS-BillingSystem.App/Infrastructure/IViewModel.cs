using ATS_BillingSystem.App.Models.Systems;
using System.Collections.Generic;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal interface IViewModel
    {
        IEnumerable<IAbonent> AbonentsCollection { get; }
        
        IAbonent Abonent { get; }

        IAbonent CalledTestAbonent { get; }

        void CreateTestAbonentsCollection(int testAbonentCount);

        void CallToTestAbonent();

        void ChoiseRandomTargetTestAbonent();

        void ConnectToPort();

        void TestAbonentConnectToPort();

        void TestAbonentDisconnectFromPort();

        void DisconnectFromPort();

        void ChoiseRandomAbonent();

        void StopCurrentCall();

        void FillTestDataToStatisticHandler();

        IEnumerable<IAbonentsHistory> GetCurrentAbonentStatistic();
    }
}
