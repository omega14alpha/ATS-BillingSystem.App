using ATS_BillingSystem.App.ATS.Interfaces;
using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Infrastructure.Constants;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal class Phone : Communicator, ITerminal
    {
        private ISim _sim;

        private IPort _port;

        private IPhoneNumber _externalPhoneNumber;

        public event EventHandler<EventArgs> OnConnectToPort;

        public event EventHandler<EventArgs> OnDisconnectFromPort;

        public event EventHandler<OutgoingCallDataEventArgs> OnPhoneStartCall;

        public event EventHandler<OutgoingCallDataEventArgs> OnPhoneStopCall;

        public event EventHandler<IncomingCallDataEventArgs> OnAcceptIncomingCall;

        public Phone(ISim sim, IPort port)
        {
            _sim = sim;
            _port = port;
        }

        public void ConnectToPort()
        {
            _port.OnSendSystemMessage += ReceivingIncomingMessages;
            _port.OnPortStartIncomingCall += AcceptIncomingCallFromPort;
            _port.OnPortStopIncomingCall += AcceptIncomingEndCallFromPort;
            OnPhoneStartCall += _port.PortStartCall;
            OnPhoneStopCall += _port.PortStopCall;
            OnConnectToPort += _port.ConnectTerminalToPort;
            OnDisconnectFromPort += _port.DisconnectTerminalFromPort;
            OnAcceptIncomingCall += _port.PortAcceptIncomingCall;
            InvokeConnectToPort(this, EventArgs.Empty);
        }

        public void DisconnectFromPort()
        {
            InvokeDisconnectFromPort(this, EventArgs.Empty);
            OnPhoneStartCall -= _port.PortStartCall;
            OnPhoneStopCall -= _port.PortStopCall;
            OnConnectToPort -= _port.ConnectTerminalToPort;
            OnAcceptIncomingCall -= _port.PortAcceptIncomingCall;
            OnDisconnectFromPort -= _port.DisconnectTerminalFromPort;
            _port.OnPortStartIncomingCall -= AcceptIncomingCallFromPort;
            _port.OnPortStopIncomingCall -= AcceptIncomingEndCallFromPort;
            _port.OnSendSystemMessage -= ReceivingIncomingMessages;
        }

        public void StartCall(IPhoneNumber calledNumber)
        {
            if (calledNumber is null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(calledNumber)));
            }

            _externalPhoneNumber = calledNumber;
            var args = new OutgoingCallDataEventArgs() { AbonentData = _sim, CalledNumber = calledNumber };
            SendSystemMessage(string.Format(InfoText.InitiateCall, _externalPhoneNumber.Number));
            InvokePhoneStartCall(this, args);
        }

        public void StopCall()
        {
            if (_externalPhoneNumber != null)
            {
                var args = new OutgoingCallDataEventArgs() { AbonentData = _sim, CalledNumber = _externalPhoneNumber };
                SendSystemMessage(string.Format(InfoText.CallCanceled));
                InvokePhoneStopCall(this, args);
                _externalPhoneNumber = null;
            }
            else
            {
                SendSystemMessage(InfoText.NoConnectionsAtTheMoment);
            }
        }

        public void AcceptIncomingCallFromPort(object sender, IncomingCallDataEventArgs args)
        {
            _externalPhoneNumber = args.SourceNumber;
            SendSystemMessage(string.Format(InfoText.YouAreBeingCalled, args.SourceNumber.Number));
        }

        public void AcceptIncomingCall() => 
            InvokeOnAcceptIncomingCall(this, new IncomingCallDataEventArgs() { SourceNumber = _externalPhoneNumber });

        public void RejectIncomingCall() => StopCall();
        

        public void AcceptIncomingEndCallFromPort(object sender, IncomingCallDataEventArgs args)
        {
            string message = string.Format(InfoText.CommunicationInterrupted, args.SourceNumber.Number);
            SendSystemMessage(message);
        }

        private void InvokeConnectToPort(object sender, EventArgs args) =>
            OnConnectToPort?.Invoke(this, args);

        private void InvokeDisconnectFromPort(object sender, EventArgs args)
        {
            if (OnDisconnectFromPort != null)
            {
                OnDisconnectFromPort.Invoke(sender, args);
                SendSystemMessage(InfoText.PhoneHasBeenDisconnected);
            }
            else
            {
                SendSystemMessage(InfoText.PhoneAlreadyDisconnected);
            }
        }

        private void InvokePhoneStartCall(object sender, OutgoingCallDataEventArgs args)
        {
            if (OnPhoneStartCall != null)
            {
                OnPhoneStartCall.Invoke(sender, args);
            }
            else
            {
                SendSystemMessage(InfoText.PhoneDisconnected);
            }
        }

        private void InvokePhoneStopCall(object sender, OutgoingCallDataEventArgs args)
        {
            if (OnPhoneStopCall != null)
            {
                OnPhoneStopCall.Invoke(sender, args);
            }
            else
            {
                SendSystemMessage(InfoText.PhoneDisconnected);
            }
        }

        private void InvokeOnAcceptIncomingCall(object sender, IncomingCallDataEventArgs args) => 
            OnAcceptIncomingCall?.Invoke(sender, args);
    }
}
