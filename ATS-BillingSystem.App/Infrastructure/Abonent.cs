﻿using ATS_BillingSystem.App.ATS;
using ATS_BillingSystem.App.BillingSystem;
using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Models.Abonents;
using ATS_BillingSystem.App.Models.Systems;
using System;
using System.Collections.Generic;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal class Abonent : Communicator, IMessager, IAbonent
    {
        private IContract _contract;

        private IPhone _phone;

        private IPort _port;

        public IContract Contract => _contract;

        public Abonent(IContract contract, IPhone phone, IPort port)
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
                throw new ArgumentNullException($"Parameter {nameof(calledNumber)} cannot be equals null!");
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
                throw new ArgumentNullException($"Parameter {nameof(statisticsCollector)} cannot be equals null!");
            }

            return statisticsCollector.GetAbonentStatistic(_contract.AbonentId, func);
        }

        public override void ReceivingIncomingMessages(object sender, SystemMessageEventArgs args)
        {
            args.Message = string.Format($" {_contract.PhoneNumber.Number} ) {args.Message}");
            InvokeSendSystemMessage(this, args);
        }
    }
}
