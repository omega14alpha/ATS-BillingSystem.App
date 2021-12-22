using ATS_BillingSystem.App.BillingSystem;
using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Models.Abonents;
using ATS_BillingSystem.App.Models.Systems;
using System;
using System.Collections.Generic;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal interface IAbonent
    {
        event EventHandler<SystemMessageEventArgs> OnSystemMessage;

        IContract Contract { get; }

        void ConnectToPort();

        void DisconectFromPort();

        void InitiateStartCall(IPhoneNumber calledNumber);

        void InitiateStopCall();

        IEnumerable<IAbonentsHistory> GetStatistic(IStatisticsCollector statisticsCollector, Func<IAbonentsHistory, bool> func);

        void ReceivePhoneSystemMessage(object sender, SystemMessageEventArgs args);
    }
}
