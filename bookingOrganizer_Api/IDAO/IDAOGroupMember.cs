using bookingOrganizer_Api.Models;

namespace bookingOrganizer_Api.IDAO
{
    public interface IDAOGroupMember
    {
        public ICollection<GroupMember> GetGroupMembersByGroupId(int groupId);

        public GroupMember GetGroupMemberById(int groupMemberId);

        public void AddGroupMember(int groupId, int userId);

        public void RemoveGroupMemberById(int groupMemberId);





    }
}
