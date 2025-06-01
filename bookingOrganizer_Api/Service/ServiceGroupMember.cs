using bookingOrganizer_Api.DAO;
using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Exceptions;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.Models;
using bookingOrganizer_Api.UTILS;

namespace bookingOrganizer_Api.Service
{
    public class ServiceGroupMember
    {
        private readonly IDAOGroupMember daoGroupMember;

        public ServiceGroupMember(IDAOGroupMember idaoGroupMember)
        {
            daoGroupMember = idaoGroupMember;
        }

        public ICollection<DTOGroupMember> GetGroupMembersByGroupId(int groupId)
        {
            try
            {
                var groupMembers = daoGroupMember.GetGroupMembersByGroupId(groupId);
                return UTILSGroupMember.ConvertGroupMembersToDTOs(groupMembers);
            }
            catch (Exception ex)
            {
                throw new GroupMemberServiceException($"Error retrieving Group Members by Group ID {groupId}. Error message: {ex.Message}", ex);
            }
        }

        public DTOGroupMember GetGroupMemberById(int groupMemberId)
        {
            try
            {
                var groupMember = daoGroupMember.GetGroupMemberById(groupMemberId);
                if (groupMember == null)
                    throw new NotFoundException($"Group Member with ID {groupMemberId} not found.");

                return UTILSGroupMember.ConvertGroupMemberToDTO(groupMember);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GroupMemberServiceException($"Error retrieving Group Member by ID {groupMemberId}. Error message: {ex.Message}", ex);
            }
        }

        public void AddGroupMember(int groupId, int userId)
        {
            try
            {
                daoGroupMember.AddGroupMember(groupId, userId);
            }
            catch (Exception ex)
            {
                throw new GroupMemberServiceException($"Error adding Group Member to Group ID {groupId} with User ID {userId}.", ex);
            }
        }

        public void RemoveGroupMember(int groupMemberId)
        {
            try
            {
                daoGroupMember.RemoveGroupMemberById(groupMemberId);
            }
            catch (KeyNotFoundException)
            {
                throw new NotFoundException($"Group Member with ID {groupMemberId} not found.");
            }
            catch (Exception ex)
            {
                throw new GroupMemberServiceException($"Error removing Group Member with ID {groupMemberId}.", ex);
            }
        }
    }
}
