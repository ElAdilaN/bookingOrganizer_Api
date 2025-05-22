namespace bookingOrganizer_Api.Exceptions
{
    public class BookingServiceException : AppException
    {
        public BookingServiceException(string message) : base(message) { }
        public BookingServiceException(string message, Exception innerException) : base(message, innerException) { }

    }
}
