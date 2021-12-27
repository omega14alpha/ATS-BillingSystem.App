using ATS_BillingSystem.App.ATS.Interfaces;
using ATS_BillingSystem.App.ATS.Models;
using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.BillingSystem.Models;
using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Infrastructure;
using ATS_BillingSystem.App.Infrastructure.Constants;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal class Station : Communicator, IStation
    {
        private IPortController _portController;

        public event EventHandler<OutgoingCallDataEventArgs> OnRecordingCallStartData;

        public event EventHandler<OutgoingCallDataEventArgs> OnRecordingCallEndData;

        public event EventHandler<IncomingCallDataEventArgs> OnIncomingCallEnd;

        public Station(IPortController portController)
        {
            _portController = portController;
        }

        public IIdentifier ReceiveIncomingCallFromPort(object sender, OutgoingCallDataEventArgs args)
        {
            var targetPort = _portController.GetPort(args.CalledNumber);
            if (targetPort != null)
            {
                if ((targetPort.State & PortState.Busy) != 0)
                {
                    SendSystemMessage(InfoText.TargetPhoneBusy);
                }
                else if (targetPort.State == PortState.Disconnect)
                {
                    SendSystemMessage(InfoText.TargetPhoneDisconected);
                }
                else
                {
                    IIdentifier callId = new CallId() { Id = IdGenerator.GetId() };
                    args.CallId = callId;
                    InvokeRecordingCallStartData(sender, args);

                    var newArgs = new IncomingCallDataEventArgs()
                    {
                        SourceNumber = args.AbonentData.PhoneNumber,
                        CallId = callId
                    };

                    targetPort.PortStartIncomingCall(this, newArgs);
                    return callId;
                }
            }

            return null;
        }

        public void ReceiveEndCurrentCallFromPort(object sender, OutgoingCallDataEventArgs args)
        {
            var port = _portController.GetPort(args.CalledNumber);
            if (port != null)
            {
                if ((port.State & PortState.Busy) != 0)
                {
                    IncomingCallEnd(args.CallId, args.AbonentData.PhoneNumber);
                    InvokeOnRecordingCallEndData(sender, args);
                }
            }
        }

        private void IncomingCallEnd(IIdentifier callId, IPhoneNumber sourceNumber)
        {
            IncomingCallDataEventArgs args = new IncomingCallDataEventArgs()
            {
                SourceNumber = sourceNumber,
                CallId = callId
            };

            InvokeOnIncomingCallEnd(this, args);
        }

        private void InvokeRecordingCallStartData(object sender, OutgoingCallDataEventArgs args) => 
            OnRecordingCallStartData?.Invoke(sender, args);
        
        private void InvokeOnRecordingCallEndData(object sender, OutgoingCallDataEventArgs args) => 
            OnRecordingCallEndData?.Invoke(sender, args);   
        
        private void InvokeOnIncomingCallEnd(object sender, IncomingCallDataEventArgs args) =>
            OnIncomingCallEnd?.Invoke(sender, args);        
    }
}
