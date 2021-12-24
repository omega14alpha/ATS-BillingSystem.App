using ATS_BillingSystem.App.EventsArgs;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal abstract class Communicator
    {
        public event EventHandler<SystemMessageEventArgs> OnSendSystemMessage;

        protected virtual void SendSystemMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Paremeter 'message' cannot be empty or equals null!", nameof(message));
            }

            var args = new SystemMessageEventArgs() { Message = message };
            InvokeSendSystemMessage(this, args);
        }

        protected virtual void InvokeSendSystemMessage(object sender, SystemMessageEventArgs args)
        {
            OnSendSystemMessage?.Invoke(sender, args);
        }
    }
}
