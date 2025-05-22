namespace bookingOrganizer_Api.Exceptions
{
    public class DAOException : AppException
    {
        public DAOException(string message) : base(message) { }
        public DAOException(string message, Exception innerException) : base(message, innerException) { }

    }
}
