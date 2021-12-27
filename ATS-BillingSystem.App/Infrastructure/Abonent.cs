using ATS_BillingSystem.App.ATS.Interfaces;
using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Infrastructure.Constants;
using ATS_BillingSystem.App.Infrastructure.Interfaces;
using System;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal class Abonent : IAbonent
    {
        private IContract _contract;

        private ITerminal _phone;

        public IContract Contract => _contract;

        public event EventHandler<SystemMessageEventArgs> OnSendSystemMessage;

        public Abonent(IContract contract, ITerminal phone)
        {
            _contract = contract;
            _phone = phone;
            _phone.OnSendSystemMessage += ReceivingIncomingMessages;
        }

        public void ConnectToPort() => _phone.ConnectToPort();
        
        public void DisconectFromPort() => _phone.DisconnectFromPort();

        public void InitiateStartCall(IPhoneNumber calledNumber)
        {
            if (calledNumber is null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(calledNumber)));
            }

            _phone.StartCall(calledNumber);
        }

        public void InitiateStopCall() => _phone.StopCall();

        public void AcceptIncomingCall() => _phone.AcceptIncomingCall();

        public void RejectIncomingCall() => _phone.RejectIncomingCall();

        public void ReceivingIncomingMessages(object sender, SystemMessageEventArgs args)
        {
            args.Message = string.Format($" {_contract.PhoneNumber.Number} ) {args.Message}");
            OnSendSystemMessage?.Invoke(this, args);
        }
    }
}
