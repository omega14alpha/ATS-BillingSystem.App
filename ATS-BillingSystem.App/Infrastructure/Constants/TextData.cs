namespace ATS_BillingSystem.App.Infrastructure.Constants
{
    internal static class InfoText
    {
        public const string TargetPhoneBusy = "Target phone number is busy.";

        public const string TargetPhoneDisconected = "Target phone number is disconected.";

        public const string InitiateCall = "Initiate call on number {0}.";

        public const string CallCanceled = "Call canceled.";

        public const string CommunacationBegin = "Communication with number {0} has begun.";

        public const string CommunicationInterrupted = "Communication with number {0} has been interrupted.";

        public const string PhoneAlreadyDisconnected = "The phone is already disconnected from the port.";

        public const string PhoneDisconnected = "The phone is disconnected from the port.";

        public const string PhoneHasBeenConnected = "A phone has been connected to the port.";

        public const string PhoneHasBeenDisconnected = "A phone has been disconnected from the port.";

        public const string PortCurrentlyBusy = "The port is currently busy!";

        public const string PortNowFree = "The port is now free!";

        public const string NoConnectionsAtTheMoment = "There are no connections at the moment.";

        public const string YouAreBeingCalled = "You are being called by a subscriber number {0}.";
    }

    internal static class ExceptionText
    {
        public const string CannotBeNull = "Parameter {0} cannot be equals null!";
    }
}
