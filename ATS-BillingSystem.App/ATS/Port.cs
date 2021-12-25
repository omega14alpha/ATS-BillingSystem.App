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
        private IPhoneNumber _number;

        private IAbonenId _abonentId;

        private PortState _state;

        public IPhoneNumber Number => _number;

        public PortState State
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _state = value;
                    var args = new PortStateEventArgs() { NewState = State };
                    InvokePortStateChanged(this, args);
                }
            }
        }

        public Func<object, CallDataEventArgs, bool> OnPortStartCall { get; set; }

        public event EventHandler<PortStateEventArgs> OnPortStateChange;

        public event EventHandler<CallDataEventArgs> OnPortStopCall;

        public event EventHandler<CallDataEventArgs> OnPortStartIncomingCall;

        public event EventHandler<CallDataEventArgs> OnPortStopIncomingCall;

        public Port(IAbonenId abonentId, IPhoneNumber number)
        {
            _abonentId = abonentId;
            _number = number;
            _state = PortState.Disconnect;
        }

        public void ConnectTerminalToPort(object sender, EventArgs args)
        {
            _state = PortState.Connect | PortState.Free;
            SendSystemMessage(InfoText.PhoneHasBeenConnected);
        }

        public void DisconnectTerminalFromPort(object sender, EventArgs args) => 
            _state = PortState.Disconnect;        

        public void PortStartCall(object sender, CallDataEventArgs args)
        {
            if ((_state & PortState.Free) != 0)
            {
                args.SourceNumber = _number;
                args.AbonentId = _abonentId;
                var isConnect = InvokeStartCall(this, args);
                if (isConnect)
                {
                    _state = PortState.Connect | PortState.Busy;
                }
            }
            else
            {
                SendSystemMessage(InfoText.PortCurrentlyBusy);
            }
        }

        public void PortStopCall(object sender, CallDataEventArgs args)
        {
            if ((_state & PortState.Busy) != 0)
            {
                _state = PortState.Connect | PortState.Free;
                args.SourceNumber = _number;
                args.AbonentId = _abonentId;
                InvokePortStopCall(this, args);
            }
            else
            {
                SendSystemMessage(InfoText.PortNowFree);
            }
        }

        public void PortStartIncomingCall(IPhoneNumber sourceNumber)
        {
            if ((_state & PortState.Free) != 0)
            {
                _state = PortState.Connect | PortState.Busy;
                var args = new CallDataEventArgs() { SourceNumber = sourceNumber };
                InvokePortStartIncomingCall(this, args);
            }
        }

        public void PortStopIncomingCall(IPhoneNumber sourceNumber)
        {
            if ((_state & PortState.Busy) != 0)
            {
                _state = PortState.Connect | PortState.Free;
                var args = new CallDataEventArgs() { SourceNumber = sourceNumber };
                InvokePortStopIncomingCall(this, args);
            }
        }

        private void InvokePortStateChanged(object sender, PortStateEventArgs args) => 
            OnPortStateChange?.Invoke(this, args);        

        private bool InvokeStartCall(object sender, CallDataEventArgs args)
        {
            var isConnect = OnPortStartCall?.Invoke(this, args);
            return isConnect.Value;
        }

        private void InvokePortStopCall(object sender, CallDataEventArgs args) => 
            OnPortStopCall?.Invoke(sender, args);        

        private void InvokePortStartIncomingCall(object sender, CallDataEventArgs args) => 
            OnPortStartIncomingCall?.Invoke(sender, args);        

        private void InvokePortStopIncomingCall(object sender, CallDataEventArgs args) => 
            OnPortStopIncomingCall?.Invoke(sender, args);        
    }
}
