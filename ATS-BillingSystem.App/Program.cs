using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Infrastructure;
using System;
using System.Linq;

namespace ATS_BillingSystem.App
{
    internal class Program
    {
        private static IViewModel _viewModel;

        static void Main(string[] args)
        {
            Init();
            WorkMenu();
        }

        private static void Init()
        {
            _viewModel = new ProgramViewModel();
            _viewModel.OnSendSystemMessage += ShowReceivedMessage;
            _viewModel.CreateTestAbonentsCollection(6);
            _viewModel.ChoiseRandomAbonent();
            _viewModel.ChoiseRandomTargetTestAbonent();
        }

        private static void WorkMenu()
        {
            while (true)
            {
                ShowAbonentsData();
                Console.WriteLine("\nOptions menu:");
                Console.WriteLine("\t1. Run test.");
                Console.WriteLine("\t2. Start call to random abonent.");
                Console.WriteLine("\t3. Stop current call.");
                Console.WriteLine("\t4. Connect the terminal to the port.");
                Console.WriteLine("\t5. Disconnect the terminal from the port.");
                Console.WriteLine("\t6. Show abonent statistic.");
                Console.WriteLine("\t7. Clear window.");
                Console.Write("\tPress any button to exit.\n");

                bool isContinue = true;
                while (isContinue)
                {
                    Console.Write("\nYour choice? ");
                    int.TryParse(Console.ReadLine(), out int choise);
                    Console.WriteLine();
                    switch (choise)
                    {
                        case 1: { RunTest(); break; }
                        case 2: { Call(); break; }
                        case 3: { EndCall(); break; }
                        case 4: { Connect(); TestAbonentConnect(); break; }
                        case 5: { Disconnect(); TestAbonentDisconnect(); break; }
                        case 6: { GetStatistic(); break; }
                        case 7:
                            {
                                Console.Clear();
                                isContinue = false;
                                break;
                            }
                        default: { Environment.Exit(0); break; }
                    }
                }
            }
        }

        private static void ShowAbonentsData()
        {
            Console.WriteLine("Data of all contracts: ");
            Console.WriteLine(
                $"\t{"Abonent Id",33} | " +
                $"{"Name",10} | " +
                $"{"Surname",10} | " +
                $"{"Number",8} | " +
                $"{"Tarrif name",12} |");
            Console.WriteLine(new string('-', 100));
            foreach (var abonent in _viewModel.AbonentsCollection)
            {
                Console.WriteLine(
                    $"\t{abonent.Contract.AbonentId.Id,33} | " +
                    $"{abonent.Contract.Name,10} | " +
                    $"{abonent.Contract.Surname,10} | " +
                    $"{abonent.Contract.PhoneNumber.Number,8} | " +
                    $"{abonent.Contract.TariffPlan.TarrifName,12} |");
            }

            Console.WriteLine("\nData of your contract: ");
            Console.WriteLine(
                $"\t{_viewModel.Abonent.Contract.AbonentId.Id,33} | " +
                $"{_viewModel.Abonent.Contract.Name,10} | " +
                $"{_viewModel.Abonent.Contract.Surname,10} | " +
                $"{_viewModel.Abonent.Contract.PhoneNumber.Number,8} | " +
                $"{_viewModel.Abonent.Contract.TariffPlan.TarrifName,12} |");

            Console.WriteLine("\nContract data of your target abonent: ");
            Console.WriteLine(
                $"\t{_viewModel.CalledTestAbonent.Contract.AbonentId.Id,33} | " +
                $"{_viewModel.CalledTestAbonent.Contract.Name,10} | " +
                $"{_viewModel.CalledTestAbonent.Contract.Surname,10} | " +
                $"{_viewModel.CalledTestAbonent.Contract.PhoneNumber.Number,8} | " +
                $"{_viewModel.CalledTestAbonent.Contract.TariffPlan.TarrifName,12} |");
        }

        private static void RunTest()
        {
            Call();
            Connect();
            Call();
            TestAbonentConnect();
            Call();
            EndCall();
            Disconnect();
            TestAbonentDisconnect();
            FillTestData();
            GetStatistic();
        }

        private static void Call() => _viewModel.CallToTestAbonent();        

        private static void EndCall() => _viewModel.StopCurrentCall();
        
        private static void Connect() => _viewModel.ConnectToPort();
        
        private static void TestAbonentConnect() => _viewModel.TestAbonentConnectToPort();        

        private static void TestAbonentDisconnect() => _viewModel.TestAbonentDisconnectFromPort();
        
        private static void Disconnect() => _viewModel.DisconnectFromPort();
        
        private static void FillTestData() => _viewModel.FillTestDataToStatisticHandler();
        
        private static void GetStatistic()
        {
            var result = _viewModel.GetCurrentAbonentStatistic();
            if (result != null && result.Any())
            {
                Console.WriteLine("\nYour calls history.");
                var tariffMinitCoas = _viewModel.Abonent.Contract.TariffPlan.PriceOfOneMinute;
                Console.WriteLine($"\n\t{"Call started in",20} | {"Call ended in",20} | {"Talk time (min)",15} | {"Cost ($)",10} | {"Target number",14} |");
                Console.WriteLine(new string('-', 100));
                foreach (var item in result)
                {
                    string talkTime = string.Format("{0:f5}", item.TalkTime);
                    string cost = string.Format("{0:f5}", item.TalkTime * tariffMinitCoas);

                    Console.WriteLine(
                        $"\t{item.BeginCallDateTime,20} | " +
                        $"{item.EndCallDateTime,20} | " +
                        $"{talkTime,15} | " +
                        $"{cost,10} | " +
                        $"{item.CalledNumber.Number,14} |");
                }
            }
            else
            {
                Console.WriteLine("Call history is empty.");
            }
        }

        private static void ShowReceivedMessage(object sender, SystemMessageEventArgs e) => 
            Console.WriteLine(e.Message);        
    }
}
