using ATS_BillingSystem.App.ATS.Interfaces;
using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Infrastructure.Constants;
using ATS_BillingSystem.App.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal class Abonent : IAbonent
    {
        private IContract _contract;

        private ITerminal _phone;

        private IPort _port;

        public IContract Contract => _contract;

        public event EventHandler<SystemMessageEventArgs> OnSendSystemMessage;

        public Abonent(IContract contract, ITerminal phone, IPort port)
        {
            _contract = contract;
            _phone = phone;
            _port = port;
            _phone.OnSendSystemMessage += ReceivingIncomingMessages;
        }

        public void ConnectToPort()
        {
            _port.OnSendSystemMessage += _phone.ReceivingIncomingMessages;
            _port.OnPortStartIncomingCall += _phone.AcceptIncomingCallFromPort;
            _port.OnPortStopIncomingCall += _phone.AcceptIncomingEndCallFromPort;
            _phone.OnPhoneStartCall += _port.PortStartCall;
            _phone.OnPhoneStopCall += _port.PortStopCall;
            _phone.OnConnectToPort += _port.ConnectTerminalToPort;
            _phone.OnDisconnectFromPort += _port.DisconnectTerminalFromPort;
            _phone.ConnectToPort();
        }

        public void DisconectFromPort()
        {
            _phone.DisconnectFromPort();
            _phone.OnConnectToPort -= _port.ConnectTerminalToPort;
            _phone.OnPhoneStartCall -= _port.PortStartCall;
            _phone.OnPhoneStopCall -= _port.PortStopCall;
            _phone.OnDisconnectFromPort -= _port.DisconnectTerminalFromPort;
            _port.OnPortStartIncomingCall -= _phone.AcceptIncomingCallFromPort;
            _port.OnPortStopIncomingCall -= _phone.AcceptIncomingEndCallFromPort;
            _port.OnSendSystemMessage -= _phone.ReceivingIncomingMessages;
        }

        public void InitiateStartCall(IPhoneNumber calledNumber)
        {
            if (calledNumber is null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(calledNumber)));
            }

            _phone.StartCall(calledNumber);
        }

        public void InitiateStopCall()
        {
            _phone.StopCall();
        }

        public IEnumerable<IAbonentsHistory> GetStatistic(IStatisticsCollector statisticsCollector, Func<IAbonentsHistory, bool> func)
        {
            if (statisticsCollector is null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(statisticsCollector)));
            }

            return statisticsCollector.GetAbonentStatistic(_contract.AbonentId, func);
        }

        public void ReceivingIncomingMessages(object sender, SystemMessageEventArgs args)
        {
            args.Message = string.Format($" {_contract.PhoneNumber.Number} ) {args.Message}");
            OnSendSystemMessage?.Invoke(this, args);
        }
    }
}
