using bookingOrganizer_Api.Exceptions;
using bookingOrganizer_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace bookingOrganizer_Api.DAO
{
    public class DAOGroupMember
    {
        public ICollection<GroupMember> GetGroupMembersByGroupId(int groupId)
        {
            try
            {
                using (var context = new BookingContext())
                {
                    return context.GroupMembers
                        .Where(gm => gm.GroupId == groupId)
                        .Include(gm => gm.User)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to get group members by group ID.", ex);
            }
        }

        public GroupMember GetGroupMemberById(int groupMemberId)
        {
            try
            {
                using (var context = new BookingContext())
                {
                    return context.GroupMembers
                        .Include(gm => gm.User)
                        .FirstOrDefault(gm => gm.GroupMemberId == groupMemberId);
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to get group member by ID.", ex);
            }
        }

        public void AddGroupMember(int groupId, int userId)
        {
            try
            {
                using (var context = new BookingContext())
                {
                    var groupMember = new GroupMember
                    {
                        GroupId = groupId,
                        UserId = userId
                    };

                    context.GroupMembers.Add(groupMember);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to add group member.", ex);
            }
        }

        public void RemoveGroupMemberById(int groupMemberId)
        {
            try
            {
                using (var context = new BookingContext())
                {
                    var groupMember = context.GroupMembers.FirstOrDefault(gm => gm.GroupMemberId == groupMemberId);
                    if (groupMember == null)
                        throw new NotFoundException($"Group member with ID {groupMemberId} was not found.");

                    context.GroupMembers.Remove(groupMember);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to remove group member.", ex);
            }
        }
    }
}
