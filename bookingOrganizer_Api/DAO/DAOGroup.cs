using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Exceptions;
using bookingOrganizer_Api.Models;

namespace bookingOrganizer_Api.DAO
{
    public class DAOGroup
    {
        public ICollection<Group> getAllGroups()
        {
            using (var context = new BookingContext())
            {
                return context.Groups.ToList();
            }
        }

        public Group getGroupById(int groupId)
        {
            using (var context = new BookingContext())
            {
                return context.Groups.Where(g => g.GroupId == groupId).FirstOrDefault();
            }
        }

        public void addGroup(Group group)
        {
            try
            {
                using (var context = new BookingContext())
                {
                    context.Groups.Add(group);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to add Group ", ex);

            }
        }

        public void RemoveGroup(int groupId)
        {
            try
            {
                using (var _context = new BookingContext())
                {
                    var group = _context.Groups.FirstOrDefault(g => g.GroupId == groupId);
                    if (group == null)
                        throw new NotFoundException($"Group with ID {groupId} was not found.");
                    _context.Groups.Remove(group);
                    _context.SaveChanges();
                }
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
                using (var context = new BookingContext())
                {
                    var existingGroup = context.Groups.FirstOrDefault(g => g.GroupId == updatedGroup.GroupId);
                    if (existingGroup == null)
                        throw new NotFoundException($"Group with ID {updatedGroup.GroupId} not found.");


                    existingGroup.BookingInfos = updatedGroup.BookingInfos;
                    existingGroup.GroupMembers = updatedGroup.GroupMembers;
                    existingGroup.GroupName = updatedGroup.GroupName;

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to Update Group ", ex);
            }
        }

    }
}
