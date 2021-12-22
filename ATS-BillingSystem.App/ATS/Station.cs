using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Infrastructure.Constants;
using ATS_BillingSystem.App.Models.Abonents;
using ATS_BillingSystem.App.Models.Systems;
using System;
using System.Linq;

namespace ATS_BillingSystem.App.ATS
{
    internal class Station : IStation
    {
        private IPortController _port;

        public event EventHandler<HistoryEventArgs> OnRecordingCallStartData;

        public event EventHandler<HistoryEventArgs> OnRecordingCallEndData;

        public event EventHandler<SystemMessageEventArgs> OnSendStationSystemMessage;

        public Station(IPortController ports)
        {
            _port = ports;
        }

        public bool ReceiveIncomingCallFromPort(object sender, CallDataEventArgs args)
        {
            IPort targetPort = _port.Ports.Where(x => x.Number == args.CalledNumber).FirstOrDefault();
            if (targetPort != null)
            {
                if ((targetPort.State & PortState.Busy) != 0)
                {
                    SendStationSystemMessage(TextData.TargetPhoneBusy);
                }
                else if (targetPort.State == PortState.Disconnect)
                {
                    SendStationSystemMessage(TextData.TargetPhoneDisconected);
                }
                else
                {
                    targetPort.PortStartIncomingCall(args.SourceNumber);
                    RecordingCallStartData(args.AbonentId, args.CalledNumber);
                    return true;
                }
            }

            return false;
        }

        public void ReceiveEndCurrentCallFromPort(object sender, CallDataEventArgs  args)
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
            if ((args.NewState & PortState.Disconnect) != 0)
            {
                var port = sender as IPort;

            }
        }

        private void RecordingCallStartData(IAbonenId abonentId, IPhoneNumber calledNumber)
        {
            if (abonentId == null)
            {
                throw new ArgumentNullException(nameof(abonentId), "Parameter 'abonentId' cannot be equals null!");
            }

            if (calledNumber == null)
            {
                throw new ArgumentNullException(nameof(calledNumber), "Parameter 'calledNumber' cannot be equals null!");
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
                throw new ArgumentNullException(nameof(abonentId), "Parameter 'abonentId' cannot be equals null!");
            }

            var args = new HistoryEventArgs()
            {
                AbonentId = abonentId,
                DateTime = DateTime.Now
            };

            InvokeOnRecordingCallEndData(this, args);
        }

        private void SendStationSystemMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(nameof(message), "Paremeter 'message' cannot be empty or equals null!");
            }

            var args = new SystemMessageEventArgs() { Message = message };
            InvokeSendStationSystemMessage(this, args);
        }

        private void InvokeRecordingCallStartData(object sender, HistoryEventArgs args)
        {
            OnRecordingCallStartData?.Invoke(sender, args);
        }

        private void InvokeOnRecordingCallEndData(object sender, HistoryEventArgs args)
        {
            OnRecordingCallEndData?.Invoke(sender, args);
        }

        private void InvokeSendStationSystemMessage(object sender, SystemMessageEventArgs args)
        {
            OnSendStationSystemMessage?.Invoke(sender, args);
        }
    }
}
