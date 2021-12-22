using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Infrastructure.Constants;
using ATS_BillingSystem.App.Models.Abonents;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal class Phone : IPhone
    {
        private IPhoneNumber _calledNumber;

        public event EventHandler<EventArgs> OnConnectToPort;

        public event EventHandler<EventArgs> OnDisconnectFromPort;

        public event EventHandler<CallDataEventArgs> OnTerminalStartCall;

        public event EventHandler<CallDataEventArgs> OnTerminalStopCall;

        public event EventHandler<SystemMessageEventArgs> OnSendTerminalSystemMessage;

        public void ConnectToPort()
        {
            InvokeConnectToPort(this, EventArgs.Empty);
        }

        public void DisconnectFromPort()
        {
            InvokeDisconnectFromPort(this, EventArgs.Empty);
        }

        public void StartCall(IPhoneNumber calledNumber)
        {
            if (calledNumber is null)
            {
                throw new ArgumentNullException($"Parameter {nameof(calledNumber)} cannot be equals null!");
            }

            _calledNumber = calledNumber;
            var args = new CallDataEventArgs() { CalledNumber = calledNumber };
            string message = string.Format(TextData.InitiateCall, _calledNumber.Number);
            SendTerminalSystemMessage(message);
            InvokeTerminalStartCall(this, args);
        }

        public void StopCall()
        {
            var args = new CallDataEventArgs() { CalledNumber = _calledNumber };
            string message = string.Format(TextData.StopCall, _calledNumber.Number);
            SendTerminalSystemMessage(message);
            InvokeTerminalStopCall(this, args);
        }

        public void AcceptIncomingCallFromPort(object sender, CallDataEventArgs args)
        {
            string message = string.Format(TextData.CommunacationBegin, args.SourceNumber.Number);
            SendTerminalSystemMessage(message);
        }

        public void AcceptIncomingEndCallFromPort(object sender, CallDataEventArgs args)
        {
            string message = string.Format(TextData.CommunicationInterrupted, args.SourceNumber.Number);
            SendTerminalSystemMessage(message);
        }

        public void ReceivingIncomingMessagesFromPort(object sender, SystemMessageEventArgs args)
        {
            InvokeSendTerminalSystemMessage(this, args);
        }

        private void SendTerminalSystemMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(nameof(message), "Paremeter 'message' cannot be empty or equals null!");
            }

            var args = new SystemMessageEventArgs() { Message = message };
            InvokeSendTerminalSystemMessage(this, args);
        }

        private void InvokeConnectToPort(object sender, EventArgs args)
        {
            OnConnectToPort?.Invoke(this, args);
        }

        private void InvokeDisconnectFromPort(object sender, EventArgs args)
        {
            if (OnDisconnectFromPort != null)
            {
                OnDisconnectFromPort.Invoke(sender, args);
            }
            else
            {
                SendTerminalSystemMessage(TextData.TerminalAlreadyDisconnected);
            }
        }

        private void InvokeTerminalStartCall(object sender, CallDataEventArgs args)
        {
            if (OnTerminalStartCall != null)
            {
                OnTerminalStartCall.Invoke(sender, args);
            }
            else
            {
                SendTerminalSystemMessage(TextData.TerminalDisconnected);
            }
        }

        private void InvokeTerminalStopCall(object sender, CallDataEventArgs args)
        {
            if (OnTerminalStopCall != null)
            {
                OnTerminalStopCall.Invoke(sender, args);
            }
            else
            {
                SendTerminalSystemMessage(TextData.TerminalDisconnected);
            }
        }

        private void InvokeSendTerminalSystemMessage(object sender, SystemMessageEventArgs args)
        {
            OnSendTerminalSystemMessage?.Invoke(sender, args);
        }
    }
}
