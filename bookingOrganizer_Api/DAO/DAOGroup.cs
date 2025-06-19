using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Exceptions;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace bookingOrganizer_Api.DAO
{
    public class DAOGroup : IDAOGroup
    {
        private readonly BookingContext _context;

        public DAOGroup(BookingContext context)
        {
            _context = context;
        }
        public ICollection<Group> getAllGroups()
        {
            try
            {
                return _context.Groups
                              .Include(g => g.BookingInfos)
                              .Include(g => g.GroupMembers)
                              .ToList();
            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to get all groups", ex);
            }
        }


        public Group getGroupById(int groupId)
        {
            try
            {
                var group = context.Groups
                                   .Include(g => g.BookingInfos)
                                   .Include(g => g.GroupMembers)
                                   .FirstOrDefault(g => g.GroupId == groupId);
                if (group == null)
                    throw new NotFoundException($"Group with ID {groupId} not found.");
                return group;
            }
            catch (Exception ex)
            {
                throw new DAOException($"Failed to get group with ID {groupId}", ex);
            }
        }

        public void addGroup(Group group)
        {
            try
            {
                _context.Groups.Add(group);
                _context.SaveChanges();

            }
            catch (DbUpdateException dbEx)
            {
                throw new DAOException("Database update failed while adding the group.", dbEx);
            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to add Group", ex);
            }
        }

        public void RemoveGroup(int groupId)
        {
            try
            {
                var group = _context.Groups.FirstOrDefault(g => g.GroupId == groupId);
                if (group == null)
                    throw new NotFoundException($"Group with ID {groupId} was not found.");
                _context.Groups.Remove(group);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to Remove group ", ex);
            }
        }


        public async Task UpdateGroup(Group updatedGroup)
        {
            try
            {
                var existingGroup = _context.Groups.FirstOrDefault(g => g.GroupId == updatedGroup.GroupId);
                if (existingGroup == null)
                    throw new NotFoundException($"Group with ID {updatedGroup.GroupId} not found.");


                existingGroup.BookingInfos = updatedGroup.BookingInfos;
                existingGroup.GroupMembers = updatedGroup.GroupMembers;
                existingGroup.GroupName = updatedGroup.GroupName;

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to Update Group ", ex);
            }
        }

    }
}
