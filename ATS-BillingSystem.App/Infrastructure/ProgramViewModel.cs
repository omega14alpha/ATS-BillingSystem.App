using ATS_BillingSystem.App.ATS;
using ATS_BillingSystem.App.BillingSystem;
using ATS_BillingSystem.App.Models.Abonents;
using ATS_BillingSystem.App.Models.Systems;
using System;
using System.Collections.Generic;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal class ProgramViewModel : Communicator, IViewModel
    {
        private readonly string[] _firstnames = new[] { "Bob", "Jecob", "Fred", "Styu", "Sten", "Ann", "Tramp" };

        private readonly string[] _surnames = new[] { "Jonson", "Calipso", "Adams", "Swift", "Simpson", "Yan", "Star", };

        private const string _tarrifName = "Light";

        private IStatisticsCollector _callStatistics;

        private PortController _portController;

        private IStation _station;

        private IList<IAbonent> _abonents;

        private IAbonent _abonent;

        private IAbonent _calledTestAbonent;

        private Random _rand;

        public IEnumerable<ISubscriber> AbonentsCollection => _abonents;

        public ISubscriber Abonent => _abonent;

        public ISubscriber CalledTestAbonent => _calledTestAbonent;

        public ProgramViewModel()
        {
            _callStatistics = new StatisticsCollector();
            _portController = new PortController();
           _station = new Station(_portController);
            _abonents = new List<IAbonent>();
            _rand = new Random(); 

            _station.OnRecordingCallStartData += _callStatistics.SaveNewCallStartData;
            _station.OnRecordingCallEndData += _callStatistics.SaveNewCallEndData;
        }

        public void CreateTestAbonentsCollection(int testAbonentCount)
        {
            ITariffPlan tariffPlan = new TariffPlanLight()
            {
                PlanId = 1,
                TarrifName = _tarrifName,
                PriceOfOneMinute = 0.7
            };

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
        

        public void FillTestDataToStatisticHandler()
        {
            ICollection<IAbonentsHistory> testCollection = new List<IAbonentsHistory>();

            for (int month = 1; month < 13; month++)
            {
                var dayCount = _rand.Next(30);
                for (int day = 1; day < dayCount; day++)
                {
                    int randomValue = _rand.Next(86401);
                    var beginDateTime = new DateTime(2021, month, day).AddSeconds(randomValue);
                    var endDateTime = new DateTime(2021, month, day).AddSeconds(_rand.Next(randomValue, randomValue + 2000));
                    var randomNumber = _abonents[_rand.Next(_abonents.Count)].Contract.PhoneNumber;
                    var tempData = new AbonentsHistory() { BeginCallDateTime = beginDateTime, EndCallDateTime = endDateTime, CalledNumber = randomNumber };

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

        public IEnumerable<IAbonentsHistory> GetCurrentAbonentStatistic()
        {
            int month = _rand.Next(DateTime.Now.Month);
            Func<IAbonentsHistory, bool> func = s => s.BeginCallDateTime.Month == month;
            try
            {
                return Abonent.GetStatistic(_callStatistics, func);
            }
            catch (Exception ex)
            {
                SendSystemMessage(ex.Message);
                return null;
            }
        }
    }
}
