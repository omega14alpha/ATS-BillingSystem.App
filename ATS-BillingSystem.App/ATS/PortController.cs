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
                throw new NullReferenceException($"Parameter '{nameof(port)}' cannot be equals null!");
            }

            _ports.Add(port);
        }

        public void RemovePort(IPort port)
        {
            if (port is null)
            {
                throw new NullReferenceException($"Parameter '{nameof(port)}' cannot be equals null!");
            }

            _ports.Remove(port);
        }
    }
}
