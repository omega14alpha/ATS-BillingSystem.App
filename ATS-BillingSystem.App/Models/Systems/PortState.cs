using System;

namespace ATS_BillingSystem.App.Models.Systems
{
    [Flags]
    internal enum PortState { Disconnect = 1, Connect = 2, Free = 4, Busy = 8 }
}
