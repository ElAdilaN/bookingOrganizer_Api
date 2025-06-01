using bookingOrganizer_Api.DTO;

namespace bookingOrganizer_Api.Repository
{
    public interface  RepoGroupMember
    {
        public ICollection<DTOGroupMember> GetGroupMembersByGroupId(int groupId);
        public DTOGroupMember GetGroupMemberById(int groupMemberId);
        public void AddGroupMember(int groupId, int userId);
        public void RemoveGroupMember(int groupMemberId);
    }
}
