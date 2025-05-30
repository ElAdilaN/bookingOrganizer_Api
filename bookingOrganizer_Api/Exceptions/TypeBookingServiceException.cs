namespace bookingOrganizer_Api.Exceptions
{
    public class TypeBookingServiceException : AppException
    {
        public TypeBookingServiceException(string message) : base(message) { }

        public TypeBookingServiceException(string message, Exception innerException) : base(message, innerException) { }

    }
}
