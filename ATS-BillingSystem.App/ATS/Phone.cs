using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Infrastructure.Constants;
using ATS_BillingSystem.App.Models.Abonents;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal class Phone : Communicator, IPhone
    {
        private IPhoneNumber _calledNumber;

        public event EventHandler<EventArgs> OnConnectToPort;

        public event EventHandler<EventArgs> OnDisconnectFromPort;

        public event EventHandler<CallDataEventArgs> OnPhoneStartCall;

        public event EventHandler<CallDataEventArgs> OnPhoneStopCall;

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
            SendSystemMessage(message);
            InvokePhoneStartCall(this, args);
        }

        public void StopCall()
        {
            if (_calledNumber != null)
            {
                var args = new CallDataEventArgs() { CalledNumber = _calledNumber };
                string message = string.Format(TextData.StopCall, _calledNumber.Number);
                _calledNumber = null;
                SendSystemMessage(message);
                InvokePhoneStopCall(this, args);
            }
            else
            {
                SendSystemMessage(TextData.NoConnectionsAtTheMoment);
            }
        }

        public void AcceptIncomingCallFromPort(object sender, CallDataEventArgs args)
        {
            string message = string.Format(TextData.CommunacationBegin, args.SourceNumber.Number);
            SendSystemMessage(message);
        }

        public void AcceptIncomingEndCallFromPort(object sender, CallDataEventArgs args)
        {
            string message = string.Format(TextData.CommunicationInterrupted, args.SourceNumber.Number);
            SendSystemMessage(message);
        }

        public void ReceivingIncomingMessagesFromPort(object sender, SystemMessageEventArgs args)
        {
            InvokeSendSystemMessage(this, args);
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
                SendSystemMessage(TextData.PhoneHasBeenDisconnected);
            }
            else
            {
                SendSystemMessage(TextData.PhoneAlreadyDisconnected);
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
                SendSystemMessage(TextData.PhoneDisconnected);
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
                SendSystemMessage(TextData.PhoneDisconnected);
            }
        }
    }
}
