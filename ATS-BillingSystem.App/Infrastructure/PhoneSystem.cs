using ATS_BillingSystem.App.ATS;
using ATS_BillingSystem.App.ATS.Interfaces;
using ATS_BillingSystem.App.BillingSystem;
using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.BillingSystem.Models;
using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Infrastructure.Constants;
using ATS_BillingSystem.App.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal class PhoneSystem : IPhoneSystem
    {
        private readonly string[] _firstnames = new[] { "Bob", "Jecob", "Fred", "Styu", "Sten", "Ann", "Tramp" };

        private readonly string[] _surnames = new[] { "Jonson", "Calipso", "Adams", "Swift", "Simpson", "Yan", "Star", };

        private const string _tarrifName = "Light";

        private IStatisticsCollector _callStatistics;

        private IBillingManager _billingManager;

        private ITariffController _tariffController;

        private IPortController _portController;

        private IStation _station;

        private IList<IAbonent> _abonents;

        private IAbonent _abonent;

        private IAbonent _calledTestAbonent;

        private Random _rand;

        public event EventHandler<SystemMessageEventArgs> OnSendSystemMessage;

        public IEnumerable<ISubscriber> AbonentsCollection => _abonents;

        public ISubscriber Abonent => _abonent;

        public ISubscriber CalledTestAbonent => _calledTestAbonent;

        public PhoneSystem()
        {
            _callStatistics = new StatisticsCollector();
            _tariffController = new TariffController();
            _billingManager = new BillingManager(_callStatistics, _tariffController);
            _portController = new PortController();
            _station = new Station(_portController);
            _abonents = new List<IAbonent>();
            _rand = new Random();

            _station.OnRecordingCallStartData += _billingManager.SaveNewCallStartData;
            _station.OnRecordingCallEndData += _billingManager.SaveNewCallEndData;
        }

        public void CreateTestAbonentsCollection(int testAbonentCount)
        {
            ITariffPlan tariffPlan = new TariffPlanLight()
            {
                PlanId = new TariffId() { Id = IdGenerator.GetId() },
                TarrifName = _tarrifName,
                PriceOfOneMinute = 0.7
            };

            _tariffController.AddNewTariffPlan(tariffPlan);
            IClientManager clientManager = new ClientManager(_station, _portController);

            for (int i = 0; i < testAbonentCount; i++)
            {
                string firstname = _firstnames[_rand.Next(0, _firstnames.Length)];
                string surname = _surnames[_rand.Next(0, _surnames.Length)];
                var abonent = clientManager.RegisterNewAbonent(firstname, surname, tariffPlan);
                _callStatistics.AddNewAbonentToCollection(abonent.Contract.AbonentId);
                _abonents.Add(abonent);
            }
        }

        public void ChoiseRandomAbonent()
        {
            _abonent = _abonents[_rand.Next(0, _abonents.Count)];
            _abonents.Remove(_abonent);
            _abonent.OnSendSystemMessage += InvokeSendSystemMessage;
        }

        public void ChoiseRandomTargetTestAbonent()
        {
            _calledTestAbonent = _abonents[_rand.Next(0, _abonents.Count)];
            _calledTestAbonent.OnSendSystemMessage += InvokeSendSystemMessage;
        }

        public void ConnectToPort() => _abonent.ConnectToPort();

        public void TestAbonentConnectToPort() => _calledTestAbonent.ConnectToPort();

        public void TestAbonentDisconnectFromPort() => _calledTestAbonent.DisconectFromPort();

        public void DisconnectFromPort() => _abonent.DisconectFromPort();

        public void CallToTestAbonent() => _abonent.InitiateStartCall(_calledTestAbonent.Contract.PhoneNumber);

        public void StopCurrentCall() => _abonent.InitiateStopCall();

        public void AbonentAcceptCall() => _abonent.AcceptIncomingCall();

        public void AbonentRejectCall() => _abonent.RejectIncomingCall();

        public void TestAbonentAcceptCall() => _calledTestAbonent.AcceptIncomingCall();

        public void TestAbonentRejectCall() => _calledTestAbonent.RejectIncomingCall();

        public void FillTestDataToStatisticHandler()
        {
            ICollection<IAbonentsHistory> testCollection = new List<IAbonentsHistory>();
            double price = _tariffController.Tariffs?.FirstOrDefault().PriceOfOneMinute ?? 0;

            for (int month = 1; month < 13; month++)
            {
                var dayCount = _rand.Next(30);
                for (int day = 1; day < dayCount; day++)
                {
                    int randomValue = _rand.Next(86401);
                    var beginDateTime = new DateTime(2021, month, day).AddSeconds(randomValue);
                    var endDateTime = new DateTime(2021, month, day).AddSeconds(_rand.Next(randomValue, randomValue + 2000));
                    var randomNumber = _abonents[_rand.Next(_abonents.Count)].Contract.PhoneNumber;
                    var talkTime = (endDateTime - beginDateTime).TotalMinutes;
                    var cost = talkTime * price;

                    var tempData = new AbonentsHistory()
                    {
                        BeginCallDateTime = beginDateTime,
                        EndCallDateTime = endDateTime,
                        CalledNumber = randomNumber,
                        TalkTime = talkTime,
                        Cost = cost
                    };

                    testCollection.Add(tempData);
                }
            }

            try
            {
                _callStatistics.AddTestAbonentData(_abonent.Contract.AbonentId, testCollection);
            }
            catch (Exception ex)
            {
                SendSystemMessage(ex.Message);
            }
        }

        public IEnumerable<IAbonentsHistory> GetStatisticsForMonth()
        {
            try
            {
                int month = _rand.Next(DateTime.Now.Month);
                return _billingManager.GetCallsStatistic(_abonent.Contract.AbonentId, s => s.BeginCallDateTime.Month == month);
            }
            catch (Exception ex)
            {
                SendSystemMessage(ex.Message);
                return null;
            }
        }

        public IEnumerable<IAbonentsHistory> GetAllStatistics()
        {
            try
            {
                return _billingManager.GetCallsStatistic(_abonent.Contract.AbonentId);
            }
            catch (Exception ex)
            {
                SendSystemMessage(ex.Message);
                return null;
            }
        }

        public void ReceivingIncomingMessages(object sender, SystemMessageEventArgs args) =>
        InvokeSendSystemMessage(this, args);

        private void SendSystemMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(message)));
            }

            var args = new SystemMessageEventArgs() { Message = message };
            InvokeSendSystemMessage(this, args);
        }

        private void InvokeSendSystemMessage(object sender, SystemMessageEventArgs args) =>
            OnSendSystemMessage?.Invoke(sender, args);
    }
}
