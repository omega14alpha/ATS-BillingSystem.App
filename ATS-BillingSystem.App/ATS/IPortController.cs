using System.Collections.Generic;

namespace ATS_BillingSystem.App.ATS
{
    internal interface IPortController
    {
        IEnumerable<IPort> Ports { get; }

        void AddNewPort(IPort port);

        void RemovePort(IPort port);
    }
}
