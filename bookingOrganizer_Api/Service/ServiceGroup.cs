using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Exceptions;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.UTILS;
using System.Threading.Tasks.Dataflow;

namespace bookingOrganizer_Api.Service
{
    public class ServiceGroup
    {
        private readonly IDAOGroup _daoGroup;

        public ServiceGroup(IDAOGroup idaoGroup)
        {
            _daoGroup = idaoGroup;
        }

        public ICollection<DTOGroup> getAllGroups()
        {
            try
            {
                return UTILSGroup.ConvertGroupsToDTOGroups(_daoGroup.getAllGroups());
            }
            catch (Exception ex)
            {
                throw new GroupServiceException("Error retrieving groups,ex");
            }
        }

        public DTOGroup getGroupById(int groupId)
        {
            try
            {
                return UTILSGroup.ConvertGroupToDTOGroup(_daoGroup.getGroupById(groupId));
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GroupServiceException($"Error retrieving Group by ID : {groupId} . Error Message :", ex);
            }

        }

        public void addGroup(DTOGroup dtoGroup)
        {
            try
            {
                Models.Group group = UTILSGroup.ConvertDTOGroupToGroup(dtoGroup);
                _daoGroup.addGroup(group);
            }
            catch (Exception ex)
            {
                throw new GroupServiceException("Errror adding Group.", ex);
            }
        }

        public void removeGroup(int groupId)
        {
            try
            {
                _daoGroup.RemoveGroup(groupId);
            }
            catch (KeyNotFoundException)
            {
                throw new NotFoundException($"Group with ID {groupId} not found.");
            }
            catch (Exception ex)
            {
                throw new GroupServiceException("Error removing grup.", ex);
            }
        }

        public async Task UpdateGroup(DTOGroup dtoGroup)
        {
            try
            {
                Models.Group group = UTILSGroup.ConvertDTOGroupToGroup(dtoGroup);
                await _daoGroup.UpdateGroup(group);
            }
            catch (KeyNotFoundException)
            {
                throw new NotFoundException($"Group with ID {dtoGroup.GroupId} not found.");
            }
            catch (Exception ex)
            {
                throw new GroupServiceException("Error updating Group.", ex);
            }
        }

    }
}
