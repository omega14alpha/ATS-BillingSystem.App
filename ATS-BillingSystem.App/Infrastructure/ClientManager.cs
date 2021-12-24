using ATS_BillingSystem.App.ATS;
using ATS_BillingSystem.App.Models.Abonents;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal class ClientManager : IClientManager
    {
        private IStation _station;

        private IPortController _portController;

        public ClientManager(IStation station, IPortController portController)
        {
            _station = station;
            _portController = portController;
        }

        public IAbonent RegisterNewAbonent(string name, string surname, ITariffPlan tariffPlan)
        {
            IContract contract = ConcludeNewContract(name, surname, tariffPlan);
            IPort port = CreateNewPort(contract);

            _portController.AddNewPort(port);

            IAbonent newAbonent = new Abonent(contract, new Phone(), port);
            return newAbonent;
        }

        private IContract ConcludeNewContract(string name, string surname, ITariffPlan tariffPlan)
        {
            IGeneratorId idGenerator = new MD5Generator();
            IAbonenId id = new AbonentId()
            {
                Id = idGenerator.GetId(name + surname)
            };

            IPhoneNumberGenerator phoneNumberGenerator = new FiveDigitNumberGenerator();
            IPhoneNumber phoneNumber = new FiveDigitNumber()
            {
                Number = phoneNumberGenerator.GetPhoneNumber()
            };

            IContract contract = new AbonentContract()
            {
                AbonentId = id,
                Name = name,
                Surname = surname,
                PhoneNumber = phoneNumber,
                TariffPlan = tariffPlan
            };

            return contract;
        }

        private IPort CreateNewPort(IContract contract)
        {
            IPort port = new Port(contract.AbonentId, contract.PhoneNumber);
            port.OnPortStateChange += _station.PortStateChanged;
            port.OnPortStartCall += _station.ReceiveIncomingCallFromPort;
            port.OnPortStopCall += _station.ReceiveEndCurrentCallFromPort;
            _station.OnSendSystemMessage += port.ReceivingIncomingMessagesFromStation;
            return port;
        }
    }
}
