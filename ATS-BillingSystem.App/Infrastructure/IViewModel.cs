using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Models.Systems;
using System;
using System.Collections.Generic;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal interface IViewModel
    {
        event EventHandler<SystemMessageEventArgs> OnSendSystemMessage;

        IEnumerable<ISubscriber> AbonentsCollection { get; }

        ISubscriber Abonent { get; }

        ISubscriber CalledTestAbonent { get; }

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
