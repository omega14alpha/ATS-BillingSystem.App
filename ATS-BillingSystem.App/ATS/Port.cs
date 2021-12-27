using ATS_BillingSystem.App.ATS.Interfaces;
using ATS_BillingSystem.App.ATS.Models;
using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Infrastructure.Constants;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal class Port : Communicator, IPort
    {
        private PortState _state;

        private IIdentifier _currentCallId;

        public PortState State => _state;

        public Func<object, OutgoingCallDataEventArgs, IIdentifier> OnPortStartCall { get; set; }

        public event EventHandler<OutgoingCallDataEventArgs> OnPortStopCall;

        public event EventHandler<IncomingCallDataEventArgs> OnPortStartIncomingCall;

        public event EventHandler<IncomingCallDataEventArgs> OnPortStopIncomingCall;

        public Port()
        {
            _state = PortState.Disconnect;
        }

        public void ConnectTerminalToPort(object sender, EventArgs args)
        {
            if (_state == PortState.Disconnect)
            {
                _state = PortState.Connect | PortState.Free;
                SendSystemMessage(InfoText.PhoneHasBeenConnected);
            }
        }

        public void DisconnectTerminalFromPort(object sender, EventArgs args) =>
            _state = PortState.Disconnect;

        public void PortStartCall(object sender, OutgoingCallDataEventArgs args)
        {
            if ((State & PortState.Free) != 0)
            {
                var callId = InvokeStartCall(this, args);
                if (callId != null)
                {
                    _currentCallId = callId;
                    _state = PortState.Connect | PortState.Busy;
                }
            }
            else
            {
                SendSystemMessage(InfoText.PortCurrentlyBusy);
            }
        }

        public void PortStopCall(object sender, OutgoingCallDataEventArgs args)
        {
            if ((State & PortState.Busy) != 0)
            {
                _state = PortState.Connect | PortState.Free;
                args.CallId = _currentCallId;
                _currentCallId = null;
                InvokePortStopCall(this, args);
            }
            else
            {
                SendSystemMessage(InfoText.PortNowFree);
            }
        }

        public void PortStartIncomingCall(object sender, IncomingCallDataEventArgs args)
        {
            _state = PortState.Connect | PortState.Busy;
            _currentCallId = args.CallId;
            InvokePortStartIncomingCall(this, args);
        }

        public void PortAcceptIncomingCall(object sender, IncomingCallDataEventArgs args) => 
            SendSystemMessage(string.Format(InfoText.CommunacationBegin, args.SourceNumber.Number));
        
        public void PortStopIncomingCall(object sender, IncomingCallDataEventArgs args)
        {
            if ((State & PortState.Busy) != 0)
            {
                _state = PortState.Connect | PortState.Free;
                args.CallId = _currentCallId;
                _currentCallId = null;
                InvokePortStopIncomingCall(this, args);
            }
        }

        private IIdentifier InvokeStartCall(object sender, OutgoingCallDataEventArgs args) => 
            OnPortStartCall?.Invoke(this, args);        

        private void InvokePortStopCall(object sender, OutgoingCallDataEventArgs args) =>
            OnPortStopCall?.Invoke(sender, args);

        private void InvokePortStartIncomingCall(object sender, IncomingCallDataEventArgs args) =>
            OnPortStartIncomingCall?.Invoke(sender, args);

        private void InvokePortStopIncomingCall(object sender, IncomingCallDataEventArgs args) =>
            OnPortStopIncomingCall?.Invoke(sender, args);
    }
}
