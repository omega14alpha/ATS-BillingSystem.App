using ATS_BillingSystem.App.BillingSystem.Interfaces;

namespace ATS_BillingSystem.App.ATS.Interfaces
{
    internal interface IPortController
    {
        void AddNewPort(IPhoneNumber number, IPort port);

        IPort GetPort(IPhoneNumber number);
    }
}
