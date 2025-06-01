namespace bookingOrganizer_Api.Exceptions
{
    public class GroupMemberServiceException : AppException
    {
        public GroupMemberServiceException(string message) : base(message) { }
        public GroupMemberServiceException(string message, Exception innerException) : base(message, innerException) { }

    }
}
