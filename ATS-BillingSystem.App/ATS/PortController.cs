using ATS_BillingSystem.App.ATS.Interfaces;
using ATS_BillingSystem.App.Infrastructure.Constants;
using System;
using System.Collections.Generic;

namespace ATS_BillingSystem.App.ATS
{
    internal class PortController : IPortController
    {
        private ICollection<IPort> _ports;

        public IEnumerable<IPort> Ports => _ports;

        public PortController()
        {
            _ports = new List<IPort>();
        }

        public void AddNewPort(IPort port)
        {
            if (port is null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(port)));
            }

            _ports.Add(port);
        }

        public void RemovePort(IPort port)
        {
            if (port is null)
            {
                throw new ArgumentNullException(string.Format(ExceptionText.CannotBeNull, nameof(port)));
            }

            _ports.Remove(port);
        }
    }
}
