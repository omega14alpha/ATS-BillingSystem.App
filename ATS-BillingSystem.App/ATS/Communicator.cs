using ATS_BillingSystem.App.EventsArgs;
using ATS_BillingSystem.App.Infrastructure.Constants;
using System;

namespace ATS_BillingSystem.App.ATS
{
    internal abstract class Communicator
    {
        public event EventHandler<SystemMessageEventArgs> OnSendSystemMessage;

        public virtual void ReceivingIncomingMessages(object sender, SystemMessageEventArgs args) =>
            InvokeSendSystemMessage(this, args);        

        protected virtual void SendSystemMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(message)));
            }

            var args = new SystemMessageEventArgs() { Message = message };
            InvokeSendSystemMessage(this, args);
        }

        protected virtual void InvokeSendSystemMessage(object sender, SystemMessageEventArgs args) => 
            OnSendSystemMessage?.Invoke(sender, args);        
    }
}
