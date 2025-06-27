using bookingOrganizer_Api.Exceptions;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace bookingOrganizer_Api.DAO
{
    public class DAOGroupMember : IDAOGroupMember
    {
        private readonly BookingContext _context;
        public DAOGroupMember(BookingContext context)
        {
            _context = context;
        }
        public ICollection<GroupMember> GetGroupMembersByGroupId(int groupId)
        {
            try
            {
                return _context.GroupMembers
                        .Where(gm => gm.GroupId == groupId)
                        .Include(gm => gm.User)
                        .ToList();

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
                return _context.GroupMembers
                   .Include(gm => gm.User)
                   .FirstOrDefault(gm => gm.GroupMemberId == groupMemberId);
            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to get group member by ID.", ex);
            }
        }

        public async Task  AddGroupMember(int groupId, int userId)
        {
            try
            {
                var groupMember = new GroupMember
                {
                    GroupId = groupId,
                    UserId = userId
                };

                _context.GroupMembers.Add(groupMember);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to add group member.", ex);
            }
        }

        public async Task  RemoveGroupMemberById(int groupMemberId)
        {
            try
            {
                var groupMember = _context.GroupMembers.FirstOrDefault(gm => gm.GroupMemberId == groupMemberId);
                if (groupMember == null)
                    throw new NotFoundException($"Group member with ID {groupMemberId} was not found.");

                _context.GroupMembers.Remove(groupMember);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to remove group member.", ex);
            }
        }
    }
}
