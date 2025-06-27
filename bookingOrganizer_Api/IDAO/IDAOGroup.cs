using System.Text.RegularExpressions;
using bookingOrganizer_Api.Models;
namespace bookingOrganizer_Api.IDAO
{
    public interface  IDAOGroup
    {
        public ICollection<Models.Group> getAllGroups();
        public Models.Group getGroupById(int groupId);

        public Task  addGroup(Models.Group group);

        public Task  RemoveGroup(int groupId);
        public Task UpdateGroup(Models.Group updatedGroup);

    }
}
