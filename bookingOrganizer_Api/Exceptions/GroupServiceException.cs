namespace bookingOrganizer_Api.Exceptions
{
    public class GroupServiceException : AppException
    {         
        public GroupServiceException(string message) : base(message) { }
        public GroupServiceException(string message, Exception innerException) : base(message, innerException) { }        
    }
}
