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

        public event EventHandler<CallDataEventArgs> OnPhoneStartCall;

        public event EventHandler<CallDataEventArgs> OnPhoneStopCall;

        public event EventHandler<SystemMessageEventArgs> OnSendPhoneSystemMessage;

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
            SendPhoneSystemMessage(message);
            InvokePhoneStartCall(this, args);
        }

        public void StopCall()
        {
            if (_calledNumber != null)
            {
                var args = new CallDataEventArgs() { CalledNumber = _calledNumber };
                string message = string.Format(TextData.StopCall, _calledNumber.Number);
                _calledNumber = null;
                SendPhoneSystemMessage(message);
                InvokePhoneStopCall(this, args);
            }
            else
            {
                SendPhoneSystemMessage(TextData.NoConnectionsAtTheMoment);
            }
        }

        public void AcceptIncomingCallFromPort(object sender, CallDataEventArgs args)
        {
            string message = string.Format(TextData.CommunacationBegin, args.SourceNumber.Number);
            SendPhoneSystemMessage(message);
        }

        public void AcceptIncomingEndCallFromPort(object sender, CallDataEventArgs args)
        {
            string message = string.Format(TextData.CommunicationInterrupted, args.SourceNumber.Number);
            SendPhoneSystemMessage(message);
        }

        public void ReceivingIncomingMessagesFromPort(object sender, SystemMessageEventArgs args)
        {
            InvokeSendPhoneSystemMessage(this, args);
        }

        private void SendPhoneSystemMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Paremeter 'message' cannot be empty or equals null!", nameof(message));
            }

            var args = new SystemMessageEventArgs() { Message = message };
            InvokeSendPhoneSystemMessage(this, args);
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
                SendPhoneSystemMessage(TextData.PhoneHasBeenDisconnected);
            }
            else
            {
                SendPhoneSystemMessage(TextData.PhoneAlreadyDisconnected);
            }
        }

        private void InvokePhoneStartCall(object sender, CallDataEventArgs args)
        {
            if (OnPhoneStartCall != null)
            {
                OnPhoneStartCall.Invoke(sender, args);
            }
            else
            {
                SendPhoneSystemMessage(TextData.PhoneDisconnected);
            }
        }

        private void InvokePhoneStopCall(object sender, CallDataEventArgs args)
        {
            if (OnPhoneStopCall != null)
            {
                OnPhoneStopCall.Invoke(sender, args);
            }
            else
            {
                SendPhoneSystemMessage(TextData.PhoneDisconnected);
            }
        }

        private void InvokeSendPhoneSystemMessage(object sender, SystemMessageEventArgs args)
        {
            OnSendPhoneSystemMessage?.Invoke(sender, args);
        }
    }
}
