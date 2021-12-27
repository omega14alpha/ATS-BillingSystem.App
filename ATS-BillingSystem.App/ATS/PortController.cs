using ATS_BillingSystem.App.ATS.Interfaces;
using ATS_BillingSystem.App.BillingSystem.Interfaces;
using ATS_BillingSystem.App.Infrastructure.Constants;
using System;
using System.Collections.Generic;

namespace ATS_BillingSystem.App.ATS
{
    internal class PortController : IPortController
    {
        private Dictionary<IPhoneNumber, IPort> _ports;

        public PortController()
        {
            _ports = new Dictionary<IPhoneNumber, IPort>();
        }
                
        public void AddNewPort(IPhoneNumber number, IPort port)
        {
            if (number is null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(number)));
            }

            if (port is null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(port)));
            }

            _ports.Add(number, port);
        }

        public IPort GetPort(IPhoneNumber number)
        {
            if (_ports.TryGetValue(number, out IPort port))
            {
                return port;
            }

            return null;
        }
    }
}
