using ATS_BillingSystem.App.ATS.Interfaces;
using ATS_BillingSystem.App.ATS.Models;
using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Infrastructure.Constants;
using ATS_BillingSystem.App.Models.Abonents;
using System;
using System.Linq;

namespace ATS_BillingSystem.App.ATS
{
    internal class Station : Communicator, IStation
    {
        private IPortController _port;

        public event EventHandler<HistoryEventArgs> OnRecordingCallStartData;

        public event EventHandler<HistoryEventArgs> OnRecordingCallEndData;

        public Station(IPortController ports)
        {
            _port = ports;
        }

        public bool ReceiveIncomingCallFromPort(object sender, CallDataEventArgs args)
        {
            IPort targetPort = _port.Ports.Where(x => x.Number == args.CalledNumber).FirstOrDefault();
            if (targetPort != null)
            {
                string message = string.Empty;
                if ((targetPort.State & PortState.Busy) != 0)
                {
                    message = InfoText.TargetPhoneBusy;
                }
                else if (targetPort.State == PortState.Disconnect)
                {
                    message = InfoText.TargetPhoneDisconected;
                }
                else
                {
                    targetPort.PortStartIncomingCall(args.SourceNumber);
                    RecordingCallStartData(args.AbonentId, args.CalledNumber);
                    return true;
                }

                SendSystemMessage(message);
            }

            return false;
        }

        public void ReceiveEndCurrentCallFromPort(object sender, CallDataEventArgs args)
        {
            IPort targetPort = _port.Ports.FirstOrDefault(x => x.Number == args.CalledNumber);
            if (targetPort != null)
            {
                if ((targetPort.State & PortState.Busy) != 0)
                {
                    targetPort.PortStopIncomingCall(args.SourceNumber);
                    RecordingCallEndData(args.AbonentId);
                }
            }
        }

        public void PortStateChanged(object sender, PortStateEventArgs args)
        {
            // Не решил какая должна быть реакция!!!
        }

        private void RecordingCallStartData(IAbonenId abonentId, IPhoneNumber calledNumber)
        {
            if (abonentId == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(abonentId)));
            }

            if (calledNumber == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(calledNumber)));
            }

            var args = new HistoryEventArgs()
            {
                AbonentId = abonentId,
                CalledNumber = calledNumber,
                DateTime = DateTime.Now
            };

            InvokeRecordingCallStartData(this, args);
        }

        private void RecordingCallEndData(IAbonenId abonentId)
        {
            if (abonentId == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(abonentId)));
            }

            var args = new HistoryEventArgs()
            {
                AbonentId = abonentId,
                DateTime = DateTime.Now
            };

            InvokeOnRecordingCallEndData(this, args);
        }

        private void InvokeRecordingCallStartData(object sender, HistoryEventArgs args) => 
            OnRecordingCallStartData?.Invoke(sender, args);
        
        private void InvokeOnRecordingCallEndData(object sender, HistoryEventArgs args) => 
            OnRecordingCallEndData?.Invoke(sender, args);        
    }
}
