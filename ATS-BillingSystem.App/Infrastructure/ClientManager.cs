using ATS_BillingSystem.App.ATS;
using ATS_BillingSystem.App.ATS.Interfaces;
using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.BillingSystem.Models;
using ATS_BillingSystem.App.Infrastructure.Interfaces;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal class ClientManager : IClientManager
    {
        private IStation _station;

        private IPortController _portController;

        private IPhoneNumberGenerator _phoneNumberGenerator;

        public ClientManager(IStation station, IPortController portController)
        {
            _station = station;
            _portController = portController;
            _phoneNumberGenerator = new FiveDigitNumberGenerator();
        }

        public IAbonent RegisterNewAbonent(string name, string surname, ITariffPlan tariffPlan)
        {
            IContract contract = ConcludeNewContract(name, surname, tariffPlan);
            IPort port = CreateNewPort(contract);
            ISim sim = new Sim()
            {
                AbonentId = contract.AbonentId,
                TariffId = contract.TariffPlan.PlanId,
                PhoneNumber = contract.PhoneNumber
            };

            _portController.AddNewPort(contract.PhoneNumber, port);

            IAbonent newAbonent = new Abonent(contract, new Phone(sim, port));
            return newAbonent;
        }

        private IContract ConcludeNewContract(string name, string surname, ITariffPlan tariffPlan)
        {
            IIdentifier abonentId = new AbonentId()
            {
                Id = IdGenerator.GetId()
            };

            IPhoneNumber phoneNumber = new FiveDigitNumber()
            {
                Number = _phoneNumberGenerator.GetPhoneNumber()
            };

            IContract contract = new AbonentContract()
            {
                AbonentId = abonentId,
                Name = name,
                Surname = surname,
                PhoneNumber = phoneNumber,
                TariffPlan = tariffPlan
            };

            return contract;
        }

        private IPort CreateNewPort(IContract contract)
        {
            IPort port = new Port();
            port.OnPortStartCall += _station.ReceiveIncomingCallFromPort;
            port.OnPortStopCall += _station.ReceiveEndCurrentCallFromPort;
            _station.OnIncomingCallEnd += port.PortStopIncomingCall;
            _station.OnSendSystemMessage += port.ReceivingIncomingMessages;
            return port;
        }
    }
}
